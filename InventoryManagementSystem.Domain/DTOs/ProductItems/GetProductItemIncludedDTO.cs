using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.ProductItems
{
    public class GetProductItemIncludedDTO
    {
        public string Id { get; set; }
        public string InventoryId { get; set; }
        public string InventoryName { get; set; }
        public string? OrderId { get; set; }

        public bool IsSelled { get; set; } = false;
        public string Color { get; set; }
        public string SerialNo { get; set; }

    }
}
