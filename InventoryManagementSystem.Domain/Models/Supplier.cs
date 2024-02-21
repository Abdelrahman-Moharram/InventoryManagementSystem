using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class Supplier : ApplicationUser
    {
        public IEnumerable<ProductItem>? ProductItems { get; set; }
    }
}
