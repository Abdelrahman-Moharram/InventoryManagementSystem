using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class Order : Base
    {
        
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }

        public virtual IEnumerable<ProductItem>? ProductItems { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
