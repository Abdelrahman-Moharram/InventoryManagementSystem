﻿using InventoryManagementSystem.Domain.Constants;
using InventoryManagementSystem.Infrastructure.Services.AuthServices;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(IRoleService roleService)
        {
            foreach (string role in Enum.GetNames(typeof(Roles)))
                foreach (string module in Enum.GetNames(typeof(Modules)))
                    await roleService.AddRoleWithPermissions(
                        role,
                        Permissions.GeneratePermissionsList(module, RoleModules.instance.cruds(role, module)).ToArray()
                        );
        }
    }
}
