using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class Inventory : Base
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual IEnumerable<Product>? Products { get; set; }
        public virtual IEnumerable<ProductsInventory>? ProductsInventory { get; set; }
        public virtual IEnumerable<ProductItem>? ProductItems { get; set; }

    }
}
