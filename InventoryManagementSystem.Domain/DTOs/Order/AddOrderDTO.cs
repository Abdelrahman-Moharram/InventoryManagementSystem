using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Order
{
    public class AddOrderDTO 
    {
        public IEnumerable<string> ProductItems { get; set; }
        
    }
}
