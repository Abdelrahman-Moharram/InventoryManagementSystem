using InventoryManagementSystem.Domain.DTOs.ProductItems;
using InventoryManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Product
{
    public class GetProductDTO 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Amount { get; set; }

        public List<SimpleModule>? ProductsInventory { get; set; }
        public List<GetProductItemIncludedDTO>? ProductItems { get; set; }

        public List<string>? Colors { get; set; }
        
        public List<string>? Images { get; set; }

        public string ModelName { get; set; }
        public string? Description { get; set; }

        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        public string BrandId { get; set; }
        public string BrandName { get; set; }


    }
}
