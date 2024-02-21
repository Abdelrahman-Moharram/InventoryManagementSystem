﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class Category : Base
    {
        public string Name { get; set; }

        public virtual ICollection<Product>? Products { get; set;  }
    }
}
