using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;

namespace InventoryManagementSystem.Infrastructure.Services.Productservices
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDTO>> GetAll();
        Task<IEnumerable<GetProductDTO>> GetAllWithBaseIncludes();
        Task<GetProductDTO> GetById(string id);
        Task<IEnumerable<GetProductDTO>> Search(string SearchQuery);
        Task<BaseResponse> AddNew(AddProductDTO newProductDTO, string CreatedBy);
        Task<BaseResponse> Update(UpdateProductDTO updateProductDTO, string UpdatedBy);
        Task<BaseResponse> Delete(string id, string DeletedBy);

    }
}