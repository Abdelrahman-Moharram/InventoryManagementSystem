using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class Brand : Base
    {
        public string Name { get; set; }

        public IEnumerable<Product>? Products { get; set; }
    }
}
