using AutoMapper;
using Endeksa.Core.DTOs;
using Endeksa.Core.Models;
using Endeksa.Core.Repositories;
using Endeksa.Core.Services;
using Endeksa.Core.UnitOfWork;
using Endeksa.Core.Utilities.Cache;
using Endeksa.Repository.Repositories;
using Endeksa.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Service.Services
{
    public class TkgmService : ITkgmService
    {
        private const string CacheRootKey = "tkgmDatasCache";
        private readonly IMapper _mapper;
        private readonly ICacheService _redisCache;
        private readonly ITkgmRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public TkgmService(IMapper mapper, ICacheService redisCache, ITkgmRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _redisCache = redisCache;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Root> AddAsync(Root entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<Root>> AddRangeAsync(IEnumerable<Root> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<Root, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<Root>> GetAllAsync()
        {
            var cacheDatas = _redisCache.GetData<IEnumerable<Root>>(CacheRootKey);
            //if (cacheDatas == null)
            //{
            //    throw new NotFoundExcepiton($"There is no any cached data");
            //}
            //if (cacheDatas == null || cacheDatas.Count() == 0)
            //{
            //    cacheDatas = await _repository.GetAll().ToListAsync();
            //    var expiryTime = DateTimeOffset.Now.AddMinutes(5);
            //    _redisCache.SetData<IEnumerable<Root>>(CacheRootKey, cacheDatas, expiryTime);
            //}
            return await Task.FromResult(cacheDatas);
        }

        public Task<Root> GetByIdAsync(int id)
        {
            var root = _redisCache.GetData<List<Root>>(CacheRootKey).FirstOrDefault(x => x.Id == id);

            if (root == null)
            {
                throw new NotFoundExcepiton($"{typeof(Root).Name}({id}) not found");
            }

            return Task.FromResult(root);
        }

        public async Task<RootDto> GetDatasByCoordinates(double Latitude, double Longitude)
        {
            RootDto rootDto = new RootDto();
            rootDto = _redisCache.GetData<RootDto>($"{Latitude - Longitude}");

            if (rootDto == null)
            {
                using (var httpClient = new HttpClient())
                {
                    string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/parsel/" + Latitude + "/" + Longitude + "/";
                    using (var response = await httpClient.GetAsync(apiurl.Replace(',', '.')))
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

                        await _repository.AddAsync(root);
                        await _unitOfWork.CommitAsync();
                        _redisCache.SetData<RootDto>($"{Latitude - Longitude}", rootDto, DateTimeOffset.Now.AddMinutes(5));
                        await CacheAllRootsAsync(CacheRootKey);
                    }
                }
            }
            return rootDto;
        }

        public async Task RemoveAsync(Root entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllRootsAsync(CacheRootKey);
        }

        public async Task RemoveRangeAsync(IEnumerable<Root> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllRootsAsync(CacheRootKey);
        }

        public async Task UpdateAsync(Root entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllRootsAsync(CacheRootKey);
        }

        public IQueryable<Root> Where(Expression<Func<Root, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<DistrictRootObject> GetDistricts(int CityId)
        {
            DistrictRootObject districtRootObject = new DistrictRootObject();
            districtRootObject = _redisCache.GetData<DistrictRootObject>($"Districts({CityId})");

            if (districtRootObject == null)
            {
                using (var httpClient = new HttpClient())
                {
                    string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api//idariYapi/ilceListe/" + CityId;
                    using (var response = await httpClient.GetAsync(apiurl))
                    {
                        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            districtRootObject = JsonConvert.DeserializeObject<DistrictRootObject>(apiResponse);
                            _redisCache.SetData<DistrictRootObject>($"Districts({CityId})",
                                districtRootObject, DateTimeOffset.Now.AddMinutes(5));
                        }
                    }
                }
            }
            return districtRootObject;
        }

        public async Task<NeighborhoodRootObject> GetNeighborhoods(int DistrictId)
        {
            NeighborhoodRootObject neighborhoodRootObject = new NeighborhoodRootObject();
            neighborhoodRootObject = _redisCache.GetData<NeighborhoodRootObject>($"Neighborhoods({DistrictId})");

            if (neighborhoodRootObject == null)
            {
                using (var httpClient = new HttpClient())
                {
                    string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/idariYapi/mahalleListe/" + DistrictId;
                    using (var response = await httpClient.GetAsync(apiurl))
                    {
                        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            neighborhoodRootObject = JsonConvert.DeserializeObject<NeighborhoodRootObject>(apiResponse);
                            _redisCache.SetData<NeighborhoodRootObject>($"Neighborhoods({DistrictId})",
                            neighborhoodRootObject, DateTimeOffset.Now.AddMinutes(5));
                        }
                    }
                }
            }
            return neighborhoodRootObject;
        }

        public async Task<CityRootObject> GetCities()
        {
            CityRootObject cityRootObject = new CityRootObject();
            cityRootObject = _redisCache.GetData<CityRootObject>("CitiesOfTurkey");

            if (cityRootObject == null)
            {
                using (var httpClient = new HttpClient())
                {
                    string apiurl = "https://cbsapi.tkgm.gov.tr/megsiswebapi.v3/api/idariYapi/ilListe";
                    using (var response = await httpClient.GetAsync(apiurl))
                    {
                        if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            cityRootObject = JsonConvert.DeserializeObject<CityRootObject>(apiResponse);
                            _redisCache.SetData<CityRootObject>("CitiesOfTurkey",
                           cityRootObject, DateTimeOffset.Now.AddMinutes(5));
                        }
                    }
                }
            }
            return cityRootObject;
        }

        public async Task<RootDto> GetParcelDatasByParcelInfo(string cityName, string districtName,
            string neighborhoodName, int parcelNo, int blockNo)
        {
            RootDto rootDto = new();
            rootDto = _redisCache.GetData<RootDto>($"{cityName}/{districtName}/{neighborhoodName}/{blockNo}/{parcelNo}");

            var cities = GetCities();
            if (cities.Result.Features.FirstOrDefault(x => x.Properties.Text == cityName) == null)
                return rootDto;
            var cityId = cities.Result.Features.FirstOrDefault(x => x.Properties.Text == cityName).Properties.Id;
            var districts = GetDistricts(cityId);
            if (districts.Result.Features.FirstOrDefault(x => x.Properties.Text == districtName) == null)
                return rootDto;
            var districtId = districts.Result.Features.FirstOrDefault(x => x.Properties.Text == districtName).Properties.Id;
            var neighborhoods = GetNeighborhoods(districtId);
            if (neighborhoods.Result.Features.FirstOrDefault(x => x.Properties.Text == neighborhoodName) == null)
                return rootDto;
            var neighborhoodId = neighborhoods.Result.Features.FirstOrDefault(x => x.Properties.Text == neighborhoodName).Properties.Id;

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

                            await _repository.AddAsync(root);
                            await _unitOfWork.CommitAsync();

                            _redisCache.SetData<RootDto>($"{cityName}/{districtName}/{neighborhoodName}/{blockNo}/{parcelNo}",
                                rootDto, DateTimeOffset.Now.AddMinutes(5));
                            await CacheAllRootsAsync(CacheRootKey);
                        }
                    }
                }
            }

            return rootDto;
        }
        public async Task CacheAllRootsAsync(string cacheKEY)
        {
            _redisCache.SetData(cacheKEY, await _repository.GetAll().ToListAsync(), DateTimeOffset.Now.AddMinutes(5));
        }
    }
}
