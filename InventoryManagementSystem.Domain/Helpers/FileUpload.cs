using InventoryManagementSystem.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Domain.Helpers
{
    public class FileUpload
    {




        /*private string UploadUserImage(IFormFile Image, string inst_Image = "img/users/user.webp")
        {

            if (Image != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);

                var imagePath = Path.Combine("wwwroot", "img", "users", fileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyToAsync(stream);
                }
                return "img/users/" + fileName;
            }
            return inst_Image;
        }*/

        public List<UploadedFile> UploadProductImages(List<IFormFile> Files, string Id, string userId)
        {
            List<UploadedFile> uploadedFiles = new();
            foreach (var file in Files)
            {

                UploadedFile uploadedFile = new()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    ProductId = Id,
                    CreatedBy = userId
                };

                var path = Path.Combine("wwwroot", "Products", file.FileName);

                using FileStream fileStream = new(path, FileMode.Create);
                file.CopyTo(fileStream);

                uploadedFiles.Add(uploadedFile);
            }

            return uploadedFiles;
        }



    }
}
