using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.ProductItems;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Helpers;
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

        [HttpGet("table")]
        [Authorize(Policy = "Permissions.Read.Product")]
        public async Task<IActionResult> DataTable() => Ok(await _productService.GetAllAsDataTable());

        [HttpGet("all")]
        [Authorize(Policy = "Permissions.Read.Product")]
        public async Task<IActionResult> ListProductsWithInners() => Ok(await _productService.GetAllWithBaseIncludes());


        [HttpGet("form/{id}")]
        [Authorize(Policy = "Permissions.Read.Product")]
        public async Task<IActionResult> ProductForForm(string id) => Ok(await _productService.GetProductForFormGetById(id));

        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Read.Product")]
        public async Task<IActionResult> Details(string id) => Ok(await _productService.GetById(id));

        [HttpGet("search")]
        public async Task<IActionResult> GetProductBySub([FromQuery] string categoryName=null, [FromQuery] string brandName = null)
            => Ok(await _productService.GetBySub(categoryName, brandName));

        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> Add([FromForm] AddProductDTO ProductDTO, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                FileUpload fileUpload = new();
                var userId = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
                var entity = _mapper.Map<Product>(ProductDTO);
                var uploadedFiles = fileUpload.UploadProductImages(Images, entity.Id, userId);
                
                BaseResponse response = await _productService.AddNew(_mapper.Map<Product>(ProductDTO), uploadedFiles, userId);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(ProductDTO);
        }

        [HttpPut("Edit/{id}")]
        [Authorize(Policy = "Permissions.Update.Product")]
        public async Task<IActionResult> Update([FromForm] UpdateProductDTO ProductDTO,[FromRoute] string id, List<IFormFile> Images)
        {
            if (ModelState.IsValid && ProductDTO.Id == id)
            {
                FileUpload fileUpload = new();
                var userId = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
                var entity = _mapper.Map<Product>(ProductDTO);
                var uploadedFiles = fileUpload.UploadProductImages(Images, entity.Id, userId);

                BaseResponse response = await _productService.Update(_mapper.Map<Product>(ProductDTO), uploadedFiles, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
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


        [HttpGet("{id}/inventories/{inevntoryId}/add")]
        [Authorize(Policy = "Permissions.Create.ProductsInventory")]
        public async Task<IActionResult> AddToInventory([FromRoute] string id,[FromRoute] string inevntoryId)
        {
            if (string.IsNullOrEmpty(id) ) return BadRequest("Invalid Product Id");

            if (string.IsNullOrEmpty(inevntoryId)) return BadRequest("Invalid inevntory Id");


            BaseResponse response = await _productService.AssignProductToInventoryAsync(id, inevntoryId, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
            if (response.IsSucceeded)
                return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("{id}/inventories/{inevntoryId}/remove")]
        [Authorize(Policy = "Permissions.Delete.ProductsInventory")]
        public async Task<IActionResult> RemoveFromInventory([FromRoute] string id, [FromRoute] string inevntoryId)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Invalid Product Id");

            if (string.IsNullOrEmpty(inevntoryId)) return BadRequest("Invalid inevntory Id");

            BaseResponse response = await _productService.RemoveProductFromInventoryAsync(id, inevntoryId, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
            if(response.IsSucceeded)
                return Ok(response);
            return BadRequest(response);
        }




        [HttpGet("{id}/items")]
        [Authorize(Policy = "Permissions.Read.ProductItem")]
        public async Task<IActionResult> Items([FromRoute] string id)
        {
            return NotFound();
        }   

        [HttpPost("{id}/items/add")]
        [Authorize(Policy = "Permissions.Create.ProductItem")]
        public async Task<IActionResult> AddItem([FromRoute] string id, [FromBody] FormProductItemDTO addProductItemDTO)
        {
            if (ModelState.IsValid )
            {
                addProductItemDTO.ProductId = id;
                var entity = _mapper.Map<ProductItem>(addProductItemDTO);
                var response = await _productService.AddProductItem(entity, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if(response.IsSucceeded)
                    return Ok(response);
                return BadRequest(response); 
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}/items/edit/{itemId}")]
        [Authorize(Policy = "Permissions.Update.ProductItem")]
        public async Task<IActionResult> EditItem([FromRoute] string id, [FromRoute] string itemId, [FromBody] FormProductItemDTO EditProductItemDTO)
        {
            ModelState.Remove("productId");
            if(EditProductItemDTO.Id != itemId)
                return BadRequest($"mismatch item id {EditProductItemDTO.Id} and form id {itemId}");
            if (ModelState.IsValid)
            {
                EditProductItemDTO.ProductId = id;

                var entity = _mapper.Map<ProductItem>(EditProductItemDTO);
                var response = await _productService.EditProductItem(entity, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response);
                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }



        [HttpDelete("{id}/items/delete/{itemId}")]
        [Authorize(Policy = "Permissions.Delete.ProductItem")]
        public async Task<IActionResult> DeleteItem([FromRoute] string id, [FromRoute] string itemId)
        {
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductItem(id, itemId, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response);
                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }


    }
}
