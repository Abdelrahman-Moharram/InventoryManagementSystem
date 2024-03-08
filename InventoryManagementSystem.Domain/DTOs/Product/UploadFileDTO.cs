using Microsoft.AspNetCore.Http;


namespace InventoryManagementSystem.Domain.DTOs.Product
{
    public class UploadFileDTO
    {
        public List<IFormFile>? Files { get; set; }
    }
}
