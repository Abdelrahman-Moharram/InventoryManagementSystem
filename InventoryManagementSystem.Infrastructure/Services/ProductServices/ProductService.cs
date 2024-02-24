using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using InventoryManagementSystem.Infrastructure.Services.Productservices;
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
        public async Task<IEnumerable<GetProductDTO>> GetAllWithBaseIncludes()
        {
            var prods = await _unitOfWork.Products.GetAllAsync(new[] { "Inventories", "ProductsInventory", "ProductItems", "Brand", "Category" });
           
            return _mapper.Map<IEnumerable<GetProductDTO>>
                (
                prods
                );
        }
        public async Task<GetProductDTO> GetById(string id)
        {
            return _mapper.Map<GetProductDTO>(await _unitOfWork.Products.GetByIdAsync(id));
        }
        public async Task<IEnumerable<GetProductDTO>> Search(string SearchQuery)
        {
            return _mapper.Map<IEnumerable<GetProductDTO>>(
                        await _unitOfWork.Products.FindAllAsync(
                            expression:
                                i => i.Name.Contains(SearchQuery) || 
                                i.Id.Contains(SearchQuery) || 
                                i.Category.Name.Contains(SearchQuery) ||
                                i.ModelName == SearchQuery,

                        includes: new[] { "Inventories", "ProductsInventories", "ProductItems" }
                    ));

        }
        public async Task<BaseResponse> AddNew(AddProductDTO newProductDTO)
        {
            if (newProductDTO.Name == null)
                return new BaseResponse { Message = "Invalid Product Name", IsSucceeded = false };

            
            try
            {
                await _unitOfWork.Products.AddAsync(_mapper.Map<Product>(newProductDTO));
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Product {newProductDTO.Name} added Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newProductDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newProductDTO.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO.Name == null)
                return new BaseResponse { Message = "Invalid Product Name", IsSucceeded = false };

            else if (await _unitOfWork.Products.Find(i => i.Id == updateProductDTO.Id) == null)
                return new BaseResponse { Message = $"Product Not Found", IsSucceeded = false };
            try
            {
                await _unitOfWork.Products.UpdateAsync(_mapper.Map<Product>(updateProductDTO));
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Product {updateProductDTO.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateProductDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateProductDTO.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id)
        {
            if (id == null)
                return new BaseResponse { Message = "Invalid Product id", IsSucceeded = false };

            var Product = await _unitOfWork.Products.GetByIdAsync(id);
            if (Product == null)
                return new BaseResponse { Message = "this Product not found", IsSucceeded = false };
            try
            {
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
    }
}
