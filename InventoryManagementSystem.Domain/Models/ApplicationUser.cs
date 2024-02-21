
using Microsoft.AspNetCore.Identity;

namespace InventoryManagementSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal Wallet { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
