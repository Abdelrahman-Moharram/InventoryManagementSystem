using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;

namespace InventoryManagementSystem.Infrastructure.Services.Productservices
{
    public interface IProductservice
    {
        Task<IEnumerable<GetProductDTO>> GetAll();
        Task<IEnumerable<GetProductDTO>> GetAllWithBaseIncludes();
        Task<GetProductDTO> GetById(string id);
        Task<IEnumerable<GetProductDTO>> Search(string SearchQuery);
        Task<BaseResponse> AddNew(AddProductDTO newProductDTO);
        Task<BaseResponse> Update(UpdateProductDTO updateProductDTO);
        Task<BaseResponse> Delete(string id);

    }
}