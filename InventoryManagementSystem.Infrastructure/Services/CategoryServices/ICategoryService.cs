using InventoryManagementSystem.Domain.DTOs;
using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;


namespace InventoryManagementSystem.Infrastructure.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDTO>> GetAll();
        Task<IEnumerable<GetCategoryDTO>> GetAllWithBaseIncludes();
        Task<GetCategoryDTO> GetById(string id);
        Task<IEnumerable<SimpleModule>> GetAsSelectList();
        Task<IEnumerable<GetCategoryDTO>> Search(string SearchQuery);
        Task<BaseResponse> AddNew(Category newCategory, string CreatedBy);
        Task<BaseResponse> Update(Category updateCategory, string UpdatedBy);
        Task<BaseResponse> Delete(string id, string DeletedBy);
    }
}
