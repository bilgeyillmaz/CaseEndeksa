using AutoMapper;
using Endeksa.API.Filters;
using Endeksa.Core.DTOs;
using Endeksa.Core.Models;
using Endeksa.Core.Services;
using Endeksa.Core.Utilities.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Endeksa.API.Controllers
{
    public class RootsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly ITkgmService _service;

        public RootsController(IMapper mapper, ITkgmService tkgmService)
        {
            _mapper = mapper;
            _service = tkgmService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> All()
        {
            var roots = await _service.GetAllAsync();
            if (roots == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, "There is no any data at redis cache."));
            }
            else
            {
                var rootsDto = _mapper.Map<List<RootModelDto>>(roots.ToList());
                return CreateActionResult(CustomResponseDto<List<RootModelDto>>.Success(200, rootsDto));
            }
        }

        [HttpGet("GetAndAddTkgmDataByLocation")]
        public async Task<IActionResult> GetDatasByCoordinates(double lat, double lon)
        {
            var rootDto = await _service.GetDatasByCoordinates(lat, lon);
            return CreateActionResult(CustomResponseDto<RootDto>.Success(200, rootDto));
        }

        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var cities = await _service.GetCities();
            if (cities == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, $"There is no any data."));
            }
            return CreateActionResult(CustomResponseDto<CityRootObject>.Success(200, cities));
        }

        [HttpGet("GetDistrictsByCityId")]
        public async Task<IActionResult> GetDistrictsByCityId(int cityId)
        {
            var districts = await _service.GetDistricts(cityId);
            if (districts == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, $"There is no any data with this ({cityId}) City Id."));
            }
            return CreateActionResult(CustomResponseDto<DistrictRootObject>.Success(200, districts));
        }

        [HttpGet("GetNeighborhoodsByDistrictId")]
        public async Task<IActionResult> GetNeighborhoodsByDistrictId(int districtId)
        {
            var neighborhoods = await _service.GetNeighborhoods(districtId);
            if (neighborhoods == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, $"There is no any data with this ({districtId}) District Id."));
            }
            return CreateActionResult(CustomResponseDto<NeighborhoodRootObject>.Success(200, neighborhoods));
        }

        [HttpGet("GetParcelDatasByParcelInfo")]
        public async Task<IActionResult> GetParcelDatasByParcelInfo(string cityName, string districtName,
            string neighborhoodName, int blockNo, int parcelNo)
        {
            var parcelDetail = await _service.GetParcelDatasByParcelInfo(cityName, districtName,
                neighborhoodName, parcelNo, blockNo);
            if (parcelDetail == null)
            {
                return CreateActionResult(CustomResponseDto<NoContentDto>
                    .Fail(404, $"There is no any data."));
            }
            return CreateActionResult(CustomResponseDto<RootDto>.Success(200, parcelDetail));
        }

        [ServiceFilter(typeof(NotFoundFilter<Root>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var root = await _service.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Root>.Success(200, root));
        }

        [HttpPost("AddData")]
        public async Task<IActionResult> Save(Root root)
        {
            await _service.AddAsync(root);
            return CreateActionResult(CustomResponseDto<Root>.Success(201, root));
        }

        [HttpPut("UpdateData")]
        public async Task<IActionResult> Update(Root root)
        {
            await _service.UpdateAsync(root);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var root = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(root);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
