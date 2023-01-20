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

        public async Task<bool> GetDatasByCoordinates(double Latitude, double Longitude)
        {
            RootDto rootDto = new RootDto();
            using (var httpClient = new HttpClient())
            {
                string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/parsel/" + Latitude + "/" + Longitude + "/";
                using (var response = await httpClient.GetAsync(apiurl.Replace(',', '.')))
                {
                    // 41.02101716111575 / 29.01263684034348 /
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    rootDto = JsonConvert.DeserializeObject<RootDto>(apiResponse);
                    //Root root = new Root()
                    //{
                    //    geometry = rootDto.geometry,
                    //    type= rootDto.type,
                    //    properties= rootDto.properties
                    //};
                    List<string> coordinatesList = new();
                    for (int i = 0; i < rootDto.geometry.coordinates.Count; i++)
                    {
                        for (int j = 0; j < rootDto.geometry.coordinates[i].Count; j++)
                        {
                            coordinatesList.Add(String.Join(",", rootDto.geometry.coordinates[i].ToArray()[j]));
                        }

                    }

                    Root root = new Root()
                    {
                        type = rootDto.type,
                        geometryType = rootDto.geometry.type,
                        coordinates = String.Join(",", coordinatesList),
                        durum = rootDto.properties.durum,
                        ilAd = rootDto.properties.ilAd,
                        ilceAd = rootDto.properties.ilceAd,
                        ilId = rootDto.properties.ilId,
                        ilceId = rootDto.properties.ilceId,
                        mahalleId = rootDto.properties.mahalleId,
                        mahalleAd = rootDto.properties.mahalleAd,
                        pafta = rootDto.properties.adaNo,
                        alan = rootDto.properties.alan,
                        mevkii = rootDto.properties.mevkii,
                        parselId = rootDto.properties.parselId,
                        nitelik = rootDto.properties.nitelik,
                        gittigiParselListe = rootDto.properties.gittigiParselListe,
                        gittigiParselSebep = rootDto.properties.gittigiParselSebep,
                        zeminKmdurum = rootDto.properties.zeminKmdurum,
                        adaNo = rootDto.properties.adaNo
                    };

                    await _tkgmRepository.AddAsync(root);
                    await _unitOfWork.CommitAsync();
                }
            }
            return true;
        }
    }
}
