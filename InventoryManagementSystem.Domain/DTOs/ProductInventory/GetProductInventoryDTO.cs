
namespace InventoryManagementSystem.Domain.DTOs.ProductInventory
{
    public class GetProductInventoryDTO : AddProductInventoryDTO
    {
        public string ProductName { get; set;}

        public string InvestoryName { get; set;}
    }
}
