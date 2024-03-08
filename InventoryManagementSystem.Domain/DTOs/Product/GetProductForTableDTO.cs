

namespace InventoryManagementSystem.Domain.DTOs.Product
{
    public class GetProductForTableDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Amount { get; set; }

        public string ProductInventories { get; set; }

        public string Colors { get; set; }


        public string ModelName { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string BrandId { get; set; }
        public string BrandName { get; set; }
    }
}
