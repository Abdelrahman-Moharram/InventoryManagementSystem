using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class ProductItem : BaseEntity
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public string InventoryId { get; set; }
        public Inventory Inventory { get; set; }
        
        public ProductsInventory ProductsInventory { get; set; }


        public string? OrderId { get; set; }
        public Order? Order { get; set; }

        public bool IsSelled { get; set; } = false;
        public string Color { get; set; }
        public string SerialNo { get; set; }
    }
}
