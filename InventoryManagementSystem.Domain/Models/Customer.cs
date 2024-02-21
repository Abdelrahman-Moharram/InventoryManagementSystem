using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class Customer : ApplicationUser
    {
        public IEnumerable<Order> Orders { get; set; }
        
    }
}
