using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAll();
        Task<IEnumerable<GetCategoryDTO>> GetAllWithBaseIncludes();
        Task<GetCategoryDTO> GetById(string id);
        Task<IEnumerable<GetCategoryDTO>> Search(string SearchQuery);

        Task<BaseResponse> AddNew(AddCategoryDTO newCategoryDTO);
        Task<BaseResponse> Update(UpdateCategoryDTO updateCategoryDTO);
        Task<BaseResponse> Delete(string id);
    }
}
