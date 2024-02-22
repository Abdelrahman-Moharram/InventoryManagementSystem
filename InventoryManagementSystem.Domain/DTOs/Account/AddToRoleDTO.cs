using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Domain.DTOs.Account
{
    public class AddToRoleDTO
    {
        
            [Required]
            public string userId { get; set; }

            [Required]
            public string roleName { get; set; }
        
    }
}
