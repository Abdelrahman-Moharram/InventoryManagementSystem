using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace InventoryManagementSystem.Infrastructure.Services.Productservices
{
    public class Productservice : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<Productservice> _logger;

        public Productservice(IUnitOfWork unitOfWork, IMapper mapper, ILogger<Productservice> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<GetProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<GetProductDTO>>(await _unitOfWork.Products.GetAllAsync());
        }
        public async Task<IEnumerable<GetProductDTO>> GetAllWithBaseIncludes() =>
            _mapper.Map<IEnumerable<GetProductDTO>>(
                await _unitOfWork.Products.GetAllAsync(new[] { "Inventories", "ProductsInventory", "ProductItems", "Brand", "Category" })
                );
        public async Task<GetProductDTO> GetById(string id) => 
            _mapper.Map<GetProductDTO>(await _unitOfWork.Products.GetByIdAsync(id));
        public async Task<IEnumerable<GetProductDTO>> Search(string SearchQuery) => 
            _mapper.Map<IEnumerable<GetProductDTO>>(
                        await _unitOfWork.Products.FindAllAsync(
                            expression:
                                i => i.Name.Contains(SearchQuery) || 
                                i.Id.Contains(SearchQuery) || 
                                i.Category.Name.Contains(SearchQuery) ||
                                i.ModelName == SearchQuery,

                        includes: new[] { "Inventories", "ProductsInventories", "ProductItems" }
                    ));
        public async Task<BaseResponse> AddNew(Product newProduct, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (newProduct.Name == null)
                return new BaseResponse { Message = "Invalid Product Name", IsSucceeded = false };

            
            try
            {
                newProduct.CreatedBy = CreatedBy;
                await _unitOfWork.Products.AddAsync(newProduct);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Product {newProduct.Name} added Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newProduct.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newProduct.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(Product updateProduct, string UpdatedBy)
        {
            if (string.IsNullOrEmpty(UpdatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (updateProduct.Name == null)
                return new BaseResponse { Message = "Invalid Product Name", IsSucceeded = false };

            else if (await _unitOfWork.Products.Find(i => i.Id == updateProduct.Id) is null)
                return new BaseResponse { Message = $"Product Not Found", IsSucceeded = false };
            try
            {

                updateProduct.UpdatedBy = UpdatedBy;
                await _unitOfWork.Products.UpdateAsync(updateProduct);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Product {updateProduct.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateProduct.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateProduct.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id, string DeletedBy)
        {
            if(string.IsNullOrEmpty(DeletedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };

            if (string.IsNullOrEmpty(id))
                return new BaseResponse { Message = "Invalid Product id", IsSucceeded = false };

            var Product = await _unitOfWork.Products.GetByIdAsync(id);
            if (Product == null)
                return new BaseResponse { Message = "this Product not found", IsSucceeded = false };
            try
            {
                Product.DeletedBy = DeletedBy;
                await _unitOfWork.Products.DeleteAsync(Product);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Product {Product.Name} Deleted Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Deleting {Product.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while Deleting {Product.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> AssignProductToInventoryAsync(string ProductId, string InventoryId, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };

            if (string.IsNullOrEmpty(ProductId) || await _unitOfWork.Products.Find(i => i.Id == ProductId ) == null)
                return new BaseResponse { IsSucceeded = false, Message = "this Product Not Found" };

            if (string.IsNullOrEmpty(InventoryId) || await _unitOfWork.Inventories.Find(i => i.Id == InventoryId) == null)
                return new BaseResponse { IsSucceeded = false, Message = "this Inventory Not Found" };

            if (await _unitOfWork.ProductsInventories.Find(i => i.ProductId == ProductId && i.InventoryId == InventoryId) != null)
                return new BaseResponse { IsSucceeded = false, Message = "Product already assigned to the inventory" };


            try
            {
                var ProductInventory = new ProductsInventory 
                {
                    ProductId = ProductId,
                    InventoryId = InventoryId,
                    CreatedBy = CreatedBy
                };
                await _unitOfWork.ProductsInventories.AddAsync(ProductInventory);
                await _unitOfWork.Save();
                return new BaseResponse { IsSucceeded = true, Message = "Product Added To Inventory Successfully !" };


            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding product with Id {ProductId} to inventory with id {InventoryId}", ex);
                return new BaseResponse { Message = "Something went wrong while adding product to inventory", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> RemoveProductFromInventoryAsync(string ProductId, string InventoryId, string DeletedBy)
        {
            if (string.IsNullOrEmpty(DeletedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (string.IsNullOrEmpty(ProductId) || await _unitOfWork.Products.Find(i => i.Id == ProductId) == null)
                return new BaseResponse { IsSucceeded = false, Message = "this Product Not Found" };

            if (string.IsNullOrEmpty(InventoryId) || await _unitOfWork.Inventories.Find(i => i.Id == InventoryId) == null)
                return new BaseResponse { IsSucceeded = false, Message = "this Inventory Not Found" };


            
            try
            {
                var productInventory = await _unitOfWork.ProductsInventories.Find(pi => pi.ProductId == ProductId && pi.InventoryId == InventoryId);
                if (productInventory is null)
                    return new BaseResponse { Message = "Product Not found in this inventory", IsSucceeded = false };

                productInventory.DeletedBy = DeletedBy;
                await _unitOfWork.ProductsInventories.DeleteAsync(productInventory);
                await _unitOfWork.Save();
                return new BaseResponse { Message = "product deleted from the inventory", IsSucceeded = true };

            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Removing product with Id {ProductId} from inventory with id {InventoryId}", ex);
                return new BaseResponse { Message = "Something went wrong while Removing the product from this inventory", IsSucceeded = false };
            }

        }
        
        public async Task<BaseResponse> AddProductItem(ProductItem productItem, string CreatedBy)
        {
            
            var product = await _unitOfWork.Products.Find(i=>i.Id == productItem.ProductId);

            if ( product is null)
                return new BaseResponse { IsSucceeded = false, Message = "This Product Not Found" };

            var inventory = await _unitOfWork.Inventories.Find(i => i.Id == productItem.InventoryId);

            if (inventory is null)
                return new BaseResponse { IsSucceeded = false, Message = "This Inventory Not Found" };

            var productInventory = await _unitOfWork.ProductsInventories.Find(i => i.ProductId == productItem.ProductId && i.InventoryId == productItem.InventoryId);
            
            if (productInventory is null)
                return new BaseResponse { IsSucceeded = false, Message = "This Product not available in this Inventory" };

            if (await _unitOfWork.ProductItems.Find(i => i.SerialNo == productItem.SerialNo, IgnoreGlobalFilters: true) != null)
                return new BaseResponse { IsSucceeded = false, Message = $"The product '{product.Name}' with serial '{productItem.SerialNo}' already exists" };

            try
            {

                product.Amount += 1;
                productInventory.Amount += 1;
                productItem.Id = Guid.NewGuid().ToString();
                productItem.CreatedBy = CreatedBy;
                productItem.ProductsInventoryId = productInventory.Id;
                await _unitOfWork.ProductItems.AddAsync(productItem);
                await _unitOfWork.Save();
                return new BaseResponse { IsSucceeded = true, Message = "Item Added Successfully!" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding item to product with Id {productItem.ProductId} and inventory with id {productItem.InventoryId}", ex);
                return new BaseResponse { Message = "Something went wrong while adding item to this product", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> EditProductItem(ProductItem productItem, string UpdatedBy)
        {

            if (await _unitOfWork.ProductItems.Find(i => i.Id == productItem.Id) is null)
                return new BaseResponse { IsSucceeded = false, Message = "This Product item is Not Found" };

            if (await _unitOfWork.ProductItems.Find(i => i.SerialNo == productItem.SerialNo && i.Id != productItem.Id) != null)
                return new BaseResponse { IsSucceeded = false, Message = "Product with this serial number already exists in anther item" };

            var productInventory = await _unitOfWork.ProductsInventories.Find(i=>i.ProductId == productItem.ProductId && i.InventoryId == productItem.InventoryId);
            if (productInventory == null)
                return new BaseResponse { IsSucceeded = false, Message = "This Product is Not found in this inventory" };

            try
            {
                productItem.UpdatedBy = UpdatedBy;
                productItem.ProductsInventoryId = productInventory.Id;
                await _unitOfWork.ProductItems.UpdateAsync(productItem);
                await _unitOfWork.Save();
                return new BaseResponse { IsSucceeded = true, Message = "Item Updated Successfully!" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Updating item to product with Id {productItem.ProductId} and inventory with id {productItem.InventoryId}", ex);
                return new BaseResponse { Message = "Something went wrong while Updating item of product", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> DeleteProductItem(string productId, string itemId, string DeletedBy)
        {
            var productItem = await _unitOfWork.ProductItems.Find(i => i.Id == itemId && i.ProductId == productId);
            if (productItem is null)
                return new BaseResponse { IsSucceeded = false, Message = "This Product item is Not Found" };

            try
            {
                var productInventory = await _unitOfWork.ProductsInventories.Find(i => i.ProductId == productItem.ProductId && i.InventoryId == productItem.InventoryId);
                var product = await _unitOfWork.Products.Find(i => i.Id == productItem.ProductId);
                product.Amount -= 1;
                productInventory.Amount -= 1;
                productItem.DeletedBy = DeletedBy;
                await _unitOfWork.ProductItems.DeleteAsync(productItem);
                await _unitOfWork.Save();
                return new BaseResponse { IsSucceeded = true, Message = "Item Removed Successfully!" };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Removing item to product with Id {productItem.ProductId} and inventory with id {productItem.InventoryId}", ex);
                return new BaseResponse { Message = "Something went wrong while Removing item of product", IsSucceeded = false };
            }
        }

    }
}
