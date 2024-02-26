using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Inventory;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Services.InventoryServices;
using InventoryManagementSystem.Infrastructure.Services.Productservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public InventoriesController(IMapper mapper, IInventoryService InventoryService, IProductService productService)
        {
            _inventoryService = InventoryService;

            _mapper = mapper;
            _productService = productService;
        }


        [HttpGet("")]
        [Authorize(Policy = "Permissions.Read.Inventory")]
        public async Task<IActionResult> Index() => Ok(await _inventoryService.GetAll());


        [HttpGet("all")]
        [Authorize(Policy = "Permissions.Read.Inventory")]
        public async Task<IActionResult> ListInventorysWithProducts() => Ok(await _inventoryService.GetAllWithBaseIncludes());


        [HttpGet("{id}")]
        [Authorize(Policy = "Permissions.Read.Inventory")]
        public async Task<IActionResult> Details(string id) => Ok(await _inventoryService.GetById(id));


        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Inventory")]
        public async Task<IActionResult> Add([FromBody] AddInventoryDTO InventoryDTO)
        {
            if (ModelState.IsValid)
            {
                BaseResponse response = await _inventoryService.AddNew(InventoryDTO, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(InventoryDTO);
        }

        [HttpPut("Edit/{id}")]
        [Authorize(Policy = "Permissions.Update.Inventory")]
        public async Task<IActionResult> Update([FromBody] UpdateInventoryDTO InventoryDTO, string id)
        {
            if (ModelState.IsValid && InventoryDTO.Id == id)
            {
                BaseResponse response = await _inventoryService.Update(InventoryDTO, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(InventoryDTO);
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Policy = "Permissions.Delete.Inventory")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id != null)
            {
                BaseResponse response = await _inventoryService.Delete(id, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);

                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest();
        }



        [HttpPost("{id}/products/{ProductId}/add")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> AddProductToInventory([FromRoute] string id, [FromRoute] string ProductId)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(ProductId)) return BadRequest(ModelState);

            return Ok(await _productService.AssignProductToInventoryAsync(ProductId, id, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value));
        }

        [HttpPost("{id}/products/{ProductId}/remove")]
        [Authorize(Policy = "Permissions.Create.Product")]
        public async Task<IActionResult> RemoveFromInventory([FromRoute] string id, [FromRoute] string ProductId)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(ProductId)) return BadRequest(ModelState);

            return Ok(await _productService.RemoveProductFromInventoryAsync(ProductId, id, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value));
        }

    }
}
