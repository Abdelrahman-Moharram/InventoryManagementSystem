﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.ProductItems
{
    public class FormProductItemDTO
    {
        public string ProductId { get; set; }
        public string InventoryId { get; set; }
        public string? Color { get; set; }
        public string? SerialNo { get; set; }

    }
}
