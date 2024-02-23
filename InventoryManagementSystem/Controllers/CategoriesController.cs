using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return Ok(_unitOfWork.Categories.GetAllAsync().Result);
        }

        [HttpGet("all/")]
        public async Task<IActionResult> ListCategoriesWithProducts()
        {
            var entity = await _unitOfWork.Categories.GetAllAsync(new[] { "Products" });
            var Categories = _mapper.Map <List<GetCategoryDTO>>(entity);

            return Ok(Categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            return Ok(await _unitOfWork.Categories.GetByIdAsync(id));
        }


        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] AddProductDTO productDTO)
        {
            return Ok();
        }

    }
}