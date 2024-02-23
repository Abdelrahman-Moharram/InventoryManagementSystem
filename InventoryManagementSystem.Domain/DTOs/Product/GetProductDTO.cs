﻿using InventoryManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Product
{
    public class GetProductDTO : UpdateProductDTO
    {
        public int Amount { get; set; }
        public string CategoryName { get; set; }

        public IEnumerable<SimpleModule> ProductsInventory { get; set; }
        public IEnumerable<ProductItem>? ProductItems { get; set; }
        public IEnumerable<string> Colors { get; set; }
    }
}