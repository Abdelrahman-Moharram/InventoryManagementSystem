
namespace InventoryManagementSystem.Domain.DTOs.Category
{
    public class CategoryDTO
    {
        public string? Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
    }
}
