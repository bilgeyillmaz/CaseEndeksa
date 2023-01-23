using AutoMapper;
using Endeksa.Core.DTOs;
using Endeksa.Core.Models;
using Endeksa.Core.Repositories;
using Endeksa.Core.Services;
using Endeksa.Core.UnitOfWork;
using Endeksa.Core.Utilities.Cache;
using Newtonsoft.Json;
using NLayer.Service.Services;

namespace Endeksa.Service.Services
{
    public class TkgmServiceWithNoCaching : Service<Root>, ITkgmService
    {
        private readonly ITkgmRepository _tkgmRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public TkgmServiceWithNoCaching(IGenericRepository<Root> repository, IUnitOfWork unitOfWork, IMapper mapper, ITkgmRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _tkgmRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<RootDto> GetDatasByCoordinates(double Latitude, double Longitude)
        {
            RootDto rootDto = new RootDto();
            using (var httpClient = new HttpClient())
            {
                string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/parsel/" + Latitude + "/" + Longitude + "/";
                using (var response = await httpClient.GetAsync(apiurl.Replace(',', '.')))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    rootDto = JsonConvert.DeserializeObject<RootDto>(apiResponse);
                    List<string> coordinatesList = new();
                    for (int i = 0; i < rootDto.Geometry.Coordinates.Count; i++)
                    {
                        for (int j = 0; j < rootDto.Geometry.Coordinates[i].Count; j++)
                        {
                            coordinatesList.Add(String.Join(",", rootDto.Geometry.Coordinates[i].ToArray()[j]));
                        }
                    }

                    Root root = new Root()
                    {
                        Type = rootDto.Type,
                        GeometryType = rootDto.Geometry.Type,
                        Coordinates = String.Join(",", coordinatesList),
                        Durum = rootDto.Properties.Durum,
                        IlAd = rootDto.Properties.IlAd,
                        IlceAd = rootDto.Properties.llceAd,
                        IlId = rootDto.Properties.IlId,
                        IlceId = rootDto.Properties.IlceId,
                        MahalleId = rootDto.Properties.MahalleId,
                        MahalleAd = rootDto.Properties.MahalleAd,
                        Pafta = rootDto.Properties.Pafta,
                        Alan = rootDto.Properties.Alan,
                        Mevkii = rootDto.Properties.Mevkii,
                        ParselId = rootDto.Properties.ParselId,
                        Nitelik = rootDto.Properties.Nitelik,
                        GittigiParselListe = rootDto.Properties.GittigiParselListe,
                        GittigiParselSebep = rootDto.Properties.GittigiParselSebep,
                        ZeminKmdurum = rootDto.Properties.ZeminKmdurum,
                        AdaNo = rootDto.Properties.AdaNo,
                        ZeminId = rootDto.Properties.ZeminId,
                        ParselNo = rootDto.Properties.ParselNo
                    };

                    await _tkgmRepository.AddAsync(root);
                    await _unitOfWork.CommitAsync();
                }
            }
            return rootDto;
        }

        public async Task<CityRootObject> GetCities()
        {
            CityRootObject cityRootObject = new CityRootObject();

            using (var httpClient = new HttpClient())
            {
                string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/idariYapi/ilListe";
                using (var response = await httpClient.GetAsync(apiurl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        cityRootObject = JsonConvert.DeserializeObject<CityRootObject>(apiResponse);
                    }
                }
            }
            return cityRootObject;
        }

        public async Task<DistrictRootObject> GetDistricts(int CityId)
        {
            DistrictRootObject districtRootObject = new DistrictRootObject();

            using (var httpClient = new HttpClient())
            {
                string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api//idariYapi/ilceListe/" + CityId;
                using (var response = await httpClient.GetAsync(apiurl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        districtRootObject = JsonConvert.DeserializeObject<DistrictRootObject>(apiResponse);
                    }
                }
            }
            return districtRootObject;
        }

        public async Task<NeighborhoodRootObject> GetNeighborhoods(int DistrictId)
        {
            NeighborhoodRootObject neighborhoodRootObject = new NeighborhoodRootObject();

            using (var httpClient = new HttpClient())
            {
                string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/idariYapi/mahalleListe/" + DistrictId;
                using (var response = await httpClient.GetAsync(apiurl))
                {
                    if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        neighborhoodRootObject = JsonConvert.DeserializeObject<NeighborhoodRootObject>(apiResponse);
                    }
                }
            }
            return neighborhoodRootObject;
        }

        public async Task<RootDto> GetParcelDatasByParcelInfo(string cityName, string districtName,
            string neighborhoodName, int parcelNo, int blockNo)
        {
            RootDto rootDto = new();

            await GetCities();
            var cityId = GetCities().Result.Features.FirstOrDefault(x => x.Properties.Text == cityName).Properties.Id;
            await GetDistricts(cityId);
            var districtId = GetDistricts(cityId).Result.Features.FirstOrDefault(x => x.Properties.Text == districtName).Properties.Id;
            await GetNeighborhoods(districtId);
            var neighborhoodId = GetNeighborhoods(districtId).Result.Features.FirstOrDefault(x => x.Properties.Text == neighborhoodName).Properties.Id;

            if (rootDto == null)
            {
                using (var httpClient = new HttpClient())
                {
                    string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/parsel/" + neighborhoodId + "/" + blockNo + "/" + parcelNo + "/";
                    using (var response = await httpClient.GetAsync(apiurl))
                    {
                        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            rootDto = JsonConvert.DeserializeObject<RootDto>(apiResponse);

                            Root root = new Root()
                            {
                                Type = rootDto.Type,
                                GeometryType = rootDto.Geometry.Type,
                                Coordinates = JsonConvert.SerializeObject(rootDto.Geometry.Coordinates),
                                Durum = rootDto.Properties.Durum,
                                IlAd = rootDto.Properties.IlAd,
                                IlceAd = rootDto.Properties.llceAd,
                                IlId = rootDto.Properties.IlId,
                                IlceId = rootDto.Properties.IlceId,
                                MahalleId = rootDto.Properties.MahalleId,
                                MahalleAd = rootDto.Properties.MahalleAd,
                                Pafta = rootDto.Properties.Pafta,
                                Alan = rootDto.Properties.Alan,
                                Mevkii = rootDto.Properties.Mevkii,
                                ParselId = rootDto.Properties.ParselId,
                                Nitelik = rootDto.Properties.Nitelik,
                                GittigiParselListe = rootDto.Properties.GittigiParselListe,
                                GittigiParselSebep = rootDto.Properties.GittigiParselSebep,
                                ZeminKmdurum = rootDto.Properties.ZeminKmdurum,
                                AdaNo = rootDto.Properties.AdaNo,
                                ZeminId = rootDto.Properties.ZeminId,
                                ParselNo = rootDto.Properties.ParselNo
                            };

                            await _tkgmRepository.AddAsync(root);
                            await _unitOfWork.CommitAsync();
                        }
                    }
                }
            }

            return rootDto;
        }
    }
}
