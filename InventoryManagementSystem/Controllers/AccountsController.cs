using InventoryManagementSystem.Domain.DTOs.Account;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Infrastructure.Services.AuthServices;
using Microsoft.AspNetCore.Mvc;


namespace InventoryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AccountsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
            if (ModelState.IsValid)
            {
                BaseResponse result = await _authService.Register(register);
                if (result.IsSucceeded)
                    return Ok(result);
                return Unauthorized(result.Message);
            }
            return BadRequest(register);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (ModelState.IsValid)
            {
                BaseResponse result = await _authService.Login(login);
                if (result.IsSucceeded)
                    return Ok(result);
                return Unauthorized(result.Message);
            }
            return BadRequest(login);
        }
    }
}