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
            //    throw new NotFoundExcepiton($"There is no any data");
            //}
            if (cacheDatas == null || cacheDatas.Count() == 0)
            {
                cacheDatas = await _repository.GetAll().ToListAsync();
                var expiryTime = DateTimeOffset.Now.AddMinutes(5);
                _redisCache.SetData<IEnumerable<Root>>(CacheRootKey, cacheDatas, expiryTime);
            }
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

        public async Task<bool> GetDatasByCoordinates(double Latitude, double Longitude)
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

                    await _repository.AddAsync(root);
                    await _unitOfWork.CommitAsync();
                    await CacheAllRootsAsync();
                }
            }
            return true;
        }

        public async Task RemoveAsync(Root entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllRootsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Root> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllRootsAsync();
        }

        public async Task UpdateAsync(Root entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllRootsAsync();
        }

        public IQueryable<Root> Where(Expression<Func<Root, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task CacheAllRootsAsync()
        {
            _redisCache.SetData(CacheRootKey, await _repository.GetAll().ToListAsync(), DateTimeOffset.Now.AddMinutes(5));
        }
    }
}
