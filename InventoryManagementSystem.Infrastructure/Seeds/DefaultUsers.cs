using InventoryManagementSystem.Domain.Constants;
using InventoryManagementSystem.Domain.DTOs.Account;
using InventoryManagementSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Infrastructure.Seeds
{
    public static class DefaultUsers
    {
        
        


        public static async Task SeedSupplierAsync(IAuthService authService, IRoleService roleService, RoleManager<IdentityRole> roleManager)
        {
            var user = await authService.AddUser(new RegisterDTO
            {
                Email = "Supplier@site.com",
                Password = "12345678",
                PhoneNumber = "01000000000",
                Username = "Supplier",
            });

            await roleService.AddUserToRole(user, Roles.Supplier.ToString());

        }

        public static async Task SeedCustomerAsync(IAuthService authService, IRoleService roleService, RoleManager<IdentityRole> roleManager)
        {
            var user = await authService.AddUser(new RegisterDTO
            {
                Email = "Customer@site.com",
                Password = "12345678",
                PhoneNumber = "01000000000",
                Username = "Customer",
            });

            await roleService.AddUserToRole(user, Roles.Customer.ToString());

        }


        public static async Task SeedAdminAsync(IAuthService authService, IRoleService roleService, RoleManager<IdentityRole> roleManager)
        {
            var user = await authService.AddUser(new RegisterDTO
            {
                Email = "admin@site.com",
                Password = "12345678",
                PhoneNumber = "01000000000",
                Username = "Admin",
            });

            await roleService.AddUserToRole(user, Roles.Admin.ToString());


            // Seed Claims
           
        }

        public static async Task SeedSuperAdminAsync(IAuthService authService, IRoleService roleService, RoleManager<IdentityRole> roleManager)
        {
            var user = await authService.AddUser(new RegisterDTO
            {
                Email = "superadmin@site.com",
                Password = "12345678",
                PhoneNumber = "01000000000",
                Username = "Super-Admin",
            });

            await roleService.AddUserToRole(user, Roles.SuperAdmin.ToString());


        }







    }
}
