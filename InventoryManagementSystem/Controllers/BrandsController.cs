﻿using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Brand;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Infrastructure.Services.BrandServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BrandsController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;
        public BrandsController(IMapper mapper, IBrandService brandService)
        {
            _mapper = mapper;
            _brandService = brandService;
        }




        [HttpGet("")]
        [Authorize(Policy = "Permissions.Read.Category")]
        public async Task<IActionResult> Index() => Ok(await _brandService.GetAll());


        [HttpGet("all/")]
        [Authorize(Policy = "Permissions.Read.Category")]
        public async Task<IActionResult> ListBrandsWithProducts() => Ok(await _brandService.GetAllWithBaseIncludes());


        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Read.Category")]
        public async Task<IActionResult> Details(string id) => Ok(await _brandService.GetById(id));


        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Category")]
        public async Task<IActionResult> Add([FromBody] AddBrandDTO brandDTO)
        {
            if(ModelState.IsValid)
            {
                brandDTO.CreatedBy = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
                BaseResponse response =  await _brandService.AddNew(brandDTO);
                if(response.IsSucceeded) 
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(brandDTO);
        }

        [HttpPost("Edit/{id}")]
        [Authorize(Policy = "Permissions.Update.Category")]
        public async Task<IActionResult> Update([FromBody] UpdateBrandDTO brandDTO, string id)
        {
            if (ModelState.IsValid && brandDTO.Id == id)
            {
                BaseResponse response = await _brandService.Update(brandDTO);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(brandDTO);
        }

        [HttpPost("Delete/{id}")]
        [Authorize(Policy = "Permissions.Delete.Category")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id != null)
            {
                BaseResponse response = await _brandService.Delete(id);

                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest();
        }

    }
}