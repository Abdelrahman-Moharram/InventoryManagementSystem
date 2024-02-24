
namespace InventoryManagementSystem.Domain.Models
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public string? CreatedBy { get; set; }
    }
}
