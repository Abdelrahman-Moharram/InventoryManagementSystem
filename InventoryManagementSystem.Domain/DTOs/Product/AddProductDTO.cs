using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Product
{
    public class AddProductDTO : BaseDTO
    {
        public string Name { get; set; }
        public string ModelName { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }

        public string CategoryId { get; set; }
    }
}
