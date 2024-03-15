using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Services.CategoryServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            _categoryService = CategoryService;
            _mapper = mapper;
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


        //GetAsSelectList
        [HttpGet("as-select")]
        [Authorize(Policy = "Permissions.Read.Category")]
        public async Task<IActionResult> AsSelect() => Ok(await _categoryService.GetAsSelectList());


        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Category")]
        public async Task<IActionResult> Add([FromBody] AddCategoryDTO CategoryDTO)
        {
            if (ModelState.IsValid)
            {
                BaseResponse response = await _categoryService.AddNew(_mapper.Map<Category>(CategoryDTO), User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
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
                BaseResponse response = await _categoryService.Update(_mapper.Map<Category>(CategoryDTO), User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
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
                BaseResponse response = await _categoryService.Delete(id, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);

                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest();
        }

        

    }
}