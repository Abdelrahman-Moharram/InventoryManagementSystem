using InventoryManagementSystem.Domain.DTOs;
using InventoryManagementSystem.Domain.DTOs.Brand;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
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
        Task<IEnumerable<GetBrandDTO>> GetAllWithBaseIncludes();
        Task<IEnumerable<SimpleModule>> GetAsSelectList();
        Task<GetBrandDTO> GetById(string id);
        Task<IEnumerable<GetBrandDTO>> Search(string SearchQuery);
        Task<BaseResponse> AddNew(Brand newBrand, string CreatedBy);
        Task<BaseResponse> Update(Brand updateBrand, string UpdatedBy);
        Task<BaseResponse> Delete(string id, string DeletedBy);
    }
}
