
namespace InventoryManagementSystem.Domain.Models
{
    public class ProductsInventory : BaseEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string InventoryId { get; set; }
        public Inventory Inventory { get; set; }

        public IEnumerable<ProductItem>? ProductItems { get; set; }
        public int Amount { get; set; } = 0;
    }
}
