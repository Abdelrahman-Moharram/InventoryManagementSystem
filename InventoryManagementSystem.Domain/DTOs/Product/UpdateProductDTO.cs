﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Product
{
    public class UpdateProductDTO : AddProductDTO
    {
        public string Id {  get; set; }
        public IEnumerable<string>? ProductImages { get; set; }
    }
}
