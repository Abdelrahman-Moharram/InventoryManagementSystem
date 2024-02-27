using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Brand;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;


namespace InventoryManagementSystem.Infrastructure.Services.BrandServices
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BrandService> _logger;
        private readonly IMapper _mapper;
        public BrandService(IUnitOfWork unitOfWork, ILogger<BrandService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetBrandDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<GetBrandDTO>>(await _unitOfWork.Brands.GetAllAsync());
        }
        public async Task<IEnumerable<GetBrandDTO>> GetAllWithBaseIncludes()
        {
            return _mapper.Map<IEnumerable<GetBrandDTO>>(await _unitOfWork.Brands.GetAllAsync(new[] { "Products" }));
        }
        public async Task<GetBrandDTO> GetById(string id)
        {
            return _mapper.Map<GetBrandDTO>(await _unitOfWork.Brands.GetByIdAsync(id));
        }
        public async Task<IEnumerable<GetBrandDTO>> Search(string SearchQuery)
        {
            return _mapper.Map<IEnumerable<GetBrandDTO>>(
                await _unitOfWork.Brands.FindAllAsync(
                        expression: i => i.Name.Contains(SearchQuery),
                        includes: new[] { "Products" }
                    ));

        }
        public async Task<BaseResponse> AddNew(Brand newBrand, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (newBrand.Name == null)
                return new BaseResponse { Message = "Invalid Brand Name", IsSucceeded = false };

            else if (await _unitOfWork.Brands.Find(i => i.Name == newBrand.Name) != null)
                return new BaseResponse { Message = $"Brand with {newBrand.Name} Name Already Exisits", IsSucceeded = false };
            try
            {
                newBrand.CreatedBy = CreatedBy;
                await _unitOfWork.Brands.AddAsync(newBrand);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Brand {newBrand.Name} added Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newBrand.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newBrand.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(Brand updateBrand, string UpdatedBy)
        {
            if (string.IsNullOrEmpty(UpdatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (updateBrand.Name == null)
                return new BaseResponse { Message = "Invalid Brand Name", IsSucceeded = false };

            else if (await _unitOfWork.Brands.Find(i => i.Id == updateBrand.Id) == null)
                return new BaseResponse { Message = $"Brand  Doesn't Exisit", IsSucceeded = false };
            try
            {
                updateBrand.UpdatedBy = UpdatedBy;
                await _unitOfWork.Brands.UpdateAsync(updateBrand);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Brand {updateBrand.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateBrand.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateBrand.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id, string DeletedBy)
        {
            if (string.IsNullOrEmpty(DeletedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (id == null)
                return new BaseResponse { Message = "Invalid Brand id", IsSucceeded = false };

            var Brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (Brand == null)
                return new BaseResponse { Message = "this Brand not found", IsSucceeded = false };
            try
            {
                Brand.DeletedBy = DeletedBy;
                await _unitOfWork.Brands.DeleteAsync(Brand);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Brand {Brand.Name} Deleted Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Deleting {Brand.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while Deleting {Brand.Name}", IsSucceeded = false };
            }
        }
    }
}
