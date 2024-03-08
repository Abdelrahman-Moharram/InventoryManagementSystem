using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Order;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Services.OrderServices;
using InventoryManagementSystem.Infrastructure.Services.Productservices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        public OrdersController(IMapper mapper, IOrderService OrderService, IProductService productService)
        {
            _mapper = mapper;
            _orderService = OrderService;
            _productService = productService;
        }




        [HttpGet("")]
        [Authorize(Roles="Admin, SuperAdmin")]
        public async Task<IActionResult> Index() => Ok(await _orderService.GetAll());


        [HttpGet("all/")]
        [Authorize(Roles="Admin, SuperAdmin")]
        public async Task<IActionResult> ListOrdersWithIncludes() => Ok(await _orderService.GetAllWithBaseIncludes());


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, SuperAdmin")]
        public async Task<IActionResult> Details(string id) => Ok(await _orderService.GetById(id));


        [HttpPost("add")]
        [Authorize(Policy = "Permissions.Create.Order")]
        public async Task<IActionResult> Add([FromBody] AddOrderDTO OrderDTO)
        {
            if (ModelState.IsValid)
            {
                BaseResponse response = await _orderService.AddNew(OrderDTO.ProductItems, User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value);
                if (response.IsSucceeded)
                    return Ok(response.Message);

                return BadRequest(response.Message);
            }
            return BadRequest(OrderDTO);
        }

        [HttpGet("my-orders")]
        [Authorize(Policy = "Permissions.Read.Order")]
        public async Task<IActionResult> UserOrders()
        {
            var user = User.Claims.FirstOrDefault(i => i.Type == "userId")?.Value;
            return Ok(await _orderService.GetUserOrders(user));
        }
        
        

    }
}
