using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using InventoryManagementSystem.Infrastructure.Services.Productservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IMapper mapper, IProductService ProductService)
        {
            _productService = ProductService;
        }

        [HttpGet("")]
        [Authorize(Policy = "Permissions.Read.Product")]
        public async Task<IActionResult> Index() => Ok(await _productService.GetAll());


        [HttpGet("all")]
        [Authorize(Policy = "Permissions.Read.Product")]
        public async Task<IActionResult> ListProductsWithInners() => Ok(await _productService.GetAllWithBaseIncludes());


        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Read.Product")]
        public async Task<IActionResult> Details(string id) => Ok(await _productService.GetById(id));


        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> Add([FromBody] AddProductDTO ProductDTO)
        {
            if (ModelState.IsValid)
            {
                ProductDTO.CreatedBy = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
                BaseResponse response = await _productService.AddNew(ProductDTO);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(ProductDTO);
        }

        [HttpPut("Edit/{id}")]
        [Authorize(Policy = "Permissions.Update.Product")]
        public async Task<IActionResult> Update([FromBody] UpdateProductDTO ProductDTO, string id)
        {
            if (ModelState.IsValid && ProductDTO.Id == id)
            {
                BaseResponse response = await _productService.Update(ProductDTO);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(ProductDTO);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Permissions.Delete.Product")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id != null)
            {
                BaseResponse response = await _productService.Delete(id);

                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest();
        }

    }
}
