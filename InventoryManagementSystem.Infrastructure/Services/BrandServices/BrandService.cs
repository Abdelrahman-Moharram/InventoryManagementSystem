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
        private readonly IMapper _mapper;
        private readonly ILogger<BrandService> _logger;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<BrandService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
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
        public async Task<BaseResponse> AddNew(AddBrandDTO newBrandDTO, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (newBrandDTO.Name == null)
                return new BaseResponse { Message = "Invalid Brand Name", IsSucceeded = false };

            else if (await _unitOfWork.Brands.Find(i => i.Name == newBrandDTO.Name) != null)
                return new BaseResponse { Message = $"Brand with {newBrandDTO.Name} Name Already Exisits", IsSucceeded = false };
            try
            {
                Brand newBrand = _mapper.Map<Brand>(newBrandDTO);
                newBrand.CreatedBy = CreatedBy;
                await _unitOfWork.Brands.AddAsync(newBrand);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Brand {newBrandDTO.Name} added Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newBrandDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newBrandDTO.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(UpdateBrandDTO updateBrandDTO, string UpdatedBy)
        {
            if (string.IsNullOrEmpty(UpdatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (updateBrandDTO.Name == null)
                return new BaseResponse { Message = "Invalid Brand Name", IsSucceeded = false };

            else if (await _unitOfWork.Brands.Find(i => i.Id == updateBrandDTO.Id) == null)
                return new BaseResponse { Message = $"Brand  Doesn't Exisit", IsSucceeded = false };
            try
            {
                var entity = _mapper.Map<Brand>(updateBrandDTO);
                entity.UpdatedBy = UpdatedBy;
                await _unitOfWork.Brands.UpdateAsync(entity);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Brand {updateBrandDTO.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateBrandDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateBrandDTO.Name}", IsSucceeded = false };
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
