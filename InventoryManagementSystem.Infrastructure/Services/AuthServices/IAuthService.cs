using InventoryManagementSystem.Domain.DTOs.Account;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;

namespace InventoryManagementSystem.Infrastructure.Services.AuthServices
{
    public interface IAuthService
    {
        Task<ApplicationUser> AddUser(RegisterDTO userDTO);
        Task<BaseResponse> Register(RegisterDTO userDTO);
        Task<BaseResponse> Login(LoginDTO loginDTO);
    }
}
