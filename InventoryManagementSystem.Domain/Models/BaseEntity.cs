using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }
    }
}
