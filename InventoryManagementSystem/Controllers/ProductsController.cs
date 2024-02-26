using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.ProductItems;
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
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IProductService ProductService)
        {
            _productService = ProductService;
            _mapper = mapper;

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
                BaseResponse response = await _productService.AddNew(ProductDTO, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
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
                BaseResponse response = await _productService.Update(ProductDTO, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(ProductDTO);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Permissions.Delete.Product")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            if (id != null)
            {
                BaseResponse response = await _productService.Delete(id, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);

                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest();
        }


        [HttpPost("{id}/inventories/{inevntoryId}/add")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> AddToInventory([FromRoute] string id,[FromRoute] string inevntoryId)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(inevntoryId)) return BadRequest("Invalid ");

            return Ok(await _productService.AssignProductToInventoryAsync(id, inevntoryId, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value));
        }

        [HttpPost("{id}/inventories/{inevntoryId}/remove")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> RemoveFromInventory([FromRoute] string id, [FromRoute] string inevntoryId)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(inevntoryId)) return BadRequest();
            return Ok(await _productService.RemoveProductFromInventoryAsync(id, inevntoryId, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value));
        }



        [HttpPost("{id}/add-item")]
        public async Task<IActionResult> AddItem([FromRoute] string id, [FromBody] FormProductItemDTO addProductItemDTO)
        {
            if (ModelState.IsValid && addProductItemDTO.ProductId == id)
            {
                var entity = _mapper.Map<ProductItem>(addProductItemDTO);
                var response = await _productService.AddProductItem(entity, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if(response.IsSucceeded)
                    return Ok(response);
                return BadRequest(response); 
            }
            return BadRequest(ModelState);
        }

        [HttpPost("{id}/edit-item/{serial}")]
        public async Task<IActionResult> EditItem([FromRoute] string id, [FromRoute] string serial, [FromBody] FormProductItemDTO EditProductItemDTO)
        {
            if (ModelState.IsValid && EditProductItemDTO.ProductId == id && EditProductItemDTO.SerialNo == serial)
            {
                var entity = _mapper.Map<ProductItem>(EditProductItemDTO);
                var response = await _productService.EditProductItem(entity, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response);
                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }
        
        [HttpPost("{id}/delete-item/{serial}")]
        public async Task<IActionResult> DeleteItem([FromRoute] string id, [FromRoute] string serial)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductItem(id, serial, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response);
                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }


    }
}
