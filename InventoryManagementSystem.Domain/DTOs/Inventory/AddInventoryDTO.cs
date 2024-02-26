using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Inventory
{
    public class AddInventoryDTO
    {
        public string Name { get; set; }
        public string? Address { get; set; }
    }
}
