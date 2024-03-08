using Microsoft.AspNetCore.Http;


namespace InventoryManagementSystem.Domain.DTOs.Product
{
    public class AddProductDTO 
    {
        public string Name { get; set; }
        public List<IFormFile>? Files { get; set; }

        public string ModelName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public string CategoryId { get; set; }
        public string BrandId { get; set; }
    }
}
