using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.DTOs.Brand
{
    public class GetBrandDTO : UpdateBrandDTO
    {
        public IEnumerable<SimpleModule>? BrandProducts { get; set; }

    }
}
