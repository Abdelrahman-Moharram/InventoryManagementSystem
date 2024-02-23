using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Brand
{
    public class UpdateBrandDTO : AddBrandDTO
    {
        public string Id { get; set; }
    }
}
