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

        [HttpGet]
        public async Task<IActionResult> All()
        {
            //var roots = await _service.GetAllAsync();
            ////var rootDtos = _mapper.Map<List<RootDto>>(roots.ToList());
            //return CreateActionResult(CustomResponseDto<List<Root>>.Success(200, roots.ToList()));

            var roots = await _service.GetAllAsync();
            var rootsDto = _mapper.Map<List<RootModelDto>>(roots.ToList());
            return CreateActionResult(CustomResponseDto<List<RootModelDto>>.Success(200, rootsDto));
        }

        [HttpGet("get-tkgm-data")]
        public async Task<IActionResult>  GetDatasByCoordinates(double lat, double lng)
        {
            await _service.GetDatasByCoordinates(lat, lng);   
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [ServiceFilter(typeof(NotFoundFilter<Root>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var root = await _service.GetByIdAsync(id);
            return CreateActionResult(CustomResponseDto<Root>.Success(200, root));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Root root)
        {
            await _service.AddAsync(root);
            return CreateActionResult(CustomResponseDto<Root>.Success(201, root));
        }


        [HttpPut]
        public async Task<IActionResult> Update(Root root)
        {
            await _service.UpdateAsync(root);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var root = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(root);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
