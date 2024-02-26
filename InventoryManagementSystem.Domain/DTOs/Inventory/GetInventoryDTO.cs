using InventoryManagementSystem.Domain.DTOs.ProductInventory;
using InventoryManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Inventory
{
    public class GetInventoryDTO : UpdateInventoryDTO
    {
        public virtual IEnumerable<SimpleModule>? Products { get; set; }
    }
}
