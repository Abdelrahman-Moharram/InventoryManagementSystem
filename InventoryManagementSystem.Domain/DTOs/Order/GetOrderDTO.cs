using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Order
{
    public class GetOrderDTO
    {
        public string? Id { get; set; }
        public string CustomerId { get; set; }
        public IEnumerable<OrderProductItemList>? ProductItems { get; set; }

        public decimal TotalPrice { get; set; }
        public int TotalAmount { get; set; }
    }
}
