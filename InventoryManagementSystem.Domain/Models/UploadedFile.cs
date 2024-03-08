using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Models
{
    public class UploadedFile : Base
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? FileName { get; set; }
        public string? ContentType { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
