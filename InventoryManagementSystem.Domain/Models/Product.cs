namespace InventoryManagementSystem.Domain.Models
{
    public class Product : Base
    {
        public string Name { get; set; }
        public string ModelName { get; set; }
        

        public string? Description { get; set; }
        public decimal Price { get; set; }

        public  string CategoryId { get; set; }
        public Category Category { get; set; }

        public string BrandId { get; set; }
        public Brand Brand { get; set; }
        public IEnumerable<Inventory>? Inventories { get; set; }

        public IEnumerable<ProductsInventory>? ProductsInventory { get; set; }
        public IEnumerable<ProductItem>? ProductItems { get; set; }
        public IEnumerable<UploadedFile>? UploadedFiles { get; set; }
        public int Amount { get; set; }

    }
}