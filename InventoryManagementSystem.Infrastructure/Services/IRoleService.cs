using InventoryManagementSystem.Domain.DTOs.Account;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;

namespace InventoryManagementSystem.Infrastructure.Services
{
    public interface IRoleService
    {
        Task<BaseResponse> AddUserToRole(ApplicationUser user, string roleName);


        Task<BaseResponse> AddToRoleAsync(AddToRoleDTO addRole);
        Task<BaseResponse> RemoveFromRoleAsync(AddToRoleDTO addRole);


        Task<BaseResponse> AddRole(string roleName);
        Task<BaseResponse> AddRoleWithPermissions(string roleName, string[] Permissions);
        Task<BaseResponse> RemoveRole(string roleName);

        Task<List<string>> GetRoleClaimsPermissions(string roleId);
        Task<List<string>> AllRoles();
        Task<List<string>> EditRoleClaimsPermissions(RolePermissionsDTO permissionsDTO);
    }
}
