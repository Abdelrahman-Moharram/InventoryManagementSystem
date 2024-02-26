using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Inventory
{
    public class UpdateInventoryDTO : AddInventoryDTO
    {
        public string Id { get; set; }
    }
}
