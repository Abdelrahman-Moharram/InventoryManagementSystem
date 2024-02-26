using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;

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
        Task<BaseResponse> AssignProductToInventoryAsync(string ProductId, string InventoryId, string CreatedBy);
        Task<BaseResponse> RemoveProductFromInventoryAsync(string ProductId, string InventoryId, string DeletedBy);
        Task<BaseResponse> AddProductItem(ProductItem productItem, string CreatedBy);
        Task<BaseResponse> EditProductItem(ProductItem productItem, string UpdatedBy);
        Task<BaseResponse> DeleteProductItem(string productId, string SerialNo, string DeletedBy);


    }
}