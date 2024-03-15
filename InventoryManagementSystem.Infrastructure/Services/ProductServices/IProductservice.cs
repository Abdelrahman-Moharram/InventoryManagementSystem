using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.ProductItems;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;

namespace InventoryManagementSystem.Infrastructure.Services.Productservices
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDTO>> GetAll();
        Task<IEnumerable<GetProductForTableDTO>> GetAllAsDataTable();
        Task<UpdateProductDTO> GetProductForFormGetById(string id);
        Task<IEnumerable<GetProductDTO>> GetAllWithBaseIncludes();
        Task<GetProductDTO> GetById(string id);
        Task<IEnumerable<GetProductDTO>> Search(string SearchQuery);
        Task<IEnumerable<GetProductDTO>> GetBySub(string CategoryName, string BrandName);

        Task<BaseResponse> AddNew(Product newProduct, List<UploadedFile> uploadedFiles, string CreatedBy);
        Task<BaseResponse> Update(Product updateProduct, List<UploadedFile> uploadedFiles, string UpdatedBy);
        Task<BaseResponse> Delete(string id, string DeletedBy);
        Task<BaseResponse> AssignProductToInventoryAsync(string ProductId, string InventoryId, string CreatedBy);
        Task<BaseResponse> RemoveProductFromInventoryAsync(string ProductId, string InventoryId, string DeletedBy);
        Task<IEnumerable<GetProductItemIncludedDTO>> GetProductItems(string ProductId);
        Task<ProductItem> GetProductItem(string ProductItemId);
        Task<BaseResponse> AddProductItem(ProductItem productItem, string CreatedBy);
        Task<BaseResponse> EditProductItem(ProductItem productItem, string UpdatedBy);
        Task<BaseResponse> DeleteProductItem(string productId, string itemId, string DeletedBy);


    }
}