using InventoryManagementSystem.Domain.DTOs.Brand;
using InventoryManagementSystem.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services.BrandServices
{
    public interface IBrandService
    {
        Task<IEnumerable<GetBrandDTO>> GetAll();
        Task<IEnumerable<GetBrandDTO>> GetAllWithBaseProducts();
        Task<GetBrandDTO> GetById(string id);
        Task<IEnumerable<GetBrandDTO>> Search(string SearchQuery);

        Task<BaseResponse> AddNew(AddBrandDTO newBrandDTO);
        Task<BaseResponse> Update(UpdateBrandDTO updateBrandDTO);
        Task<BaseResponse> Delete(string id);
    }
}
