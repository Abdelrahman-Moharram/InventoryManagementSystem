using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using InventoryManagementSystem.Infrastructure.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(IMapper mapper, ICategoryService CategoryService)
        {
            _mapper = mapper;
            _categoryService = CategoryService;
        }




        [HttpGet("")]
        [Authorize(Policy = "Permissions.Read.Category")]
        public async Task<IActionResult> Index() => Ok(await _categoryService.GetAll());


        [HttpGet("all/")]
        [Authorize(Policy = "Permissions.Read.Category")]
        public async Task<IActionResult> ListCategorysWithProducts() => Ok(await _categoryService.GetAllWithBaseIncludes());


        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Read.Category")]
        public async Task<IActionResult> Details(string id) => Ok(await _categoryService.GetById(id));


        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Category")]
        public async Task<IActionResult> Add([FromBody] AddCategoryDTO CategoryDTO)
        {
            if (ModelState.IsValid)
            {
                CategoryDTO.CreatedBy = User.Claims.FirstOrDefault(i=>i.Type == "userId")?.Value;
                BaseResponse response = await _categoryService.AddNew(CategoryDTO);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(CategoryDTO);
        }

        [HttpPut("Edit/{id}")]
        [Authorize(Policy = "Permissions.Update.Category")]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryDTO CategoryDTO, string id)
        {
            if (ModelState.IsValid && CategoryDTO.Id == id)
            {
                BaseResponse response = await _categoryService.Update(CategoryDTO);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(CategoryDTO);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Permissions.Delete.Category")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id != null)
            {
                BaseResponse response = await _categoryService.Delete(id);

                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest();
        }

    }
}