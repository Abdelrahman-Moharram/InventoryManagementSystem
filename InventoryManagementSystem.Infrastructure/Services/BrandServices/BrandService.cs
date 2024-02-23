using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Brand;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using InventoryManagementSystem.Infrastructure.Services.BrandServices;
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
        public async Task<IEnumerable<GetBrandDTO>> GetAllWithBaseProducts()
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
                        i => i.Name.Contains(SearchQuery) || i.Id.Contains(SearchQuery)
                    ));

        }
        public async Task<BaseResponse> AddNew(AddBrandDTO newBrandDTO)
        {
            if (newBrandDTO.Name == null)
                return new BaseResponse { Message = "Invalid Brand Name", IsSucceeded = false };

            else if (await _unitOfWork.Brands.Find(i => i.Name == newBrandDTO.Name) != null)
                return new BaseResponse { Message = $"Brand with {newBrandDTO.Name} Name Already Exisits", IsSucceeded = false };
            try
            {
                await _unitOfWork.Brands.AddAsync(_mapper.Map<Brand>(newBrandDTO));
                return new BaseResponse { Message = $"Brand {newBrandDTO.Name} added Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newBrandDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newBrandDTO.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(UpdateBrandDTO updateBrandDTO)
        {
            if (updateBrandDTO.Name == null)
                return new BaseResponse { Message = "Invalid Brand Name", IsSucceeded = false };

            else if (await _unitOfWork.Brands.Find(i => i.Name == updateBrandDTO.Name) == null)
                return new BaseResponse { Message = $"Brand with {updateBrandDTO.Name} Name Doesn't Exisit", IsSucceeded = false };
            try
            {
                await _unitOfWork.Brands.UpdateAsync(_mapper.Map<Brand>(updateBrandDTO));
                return new BaseResponse { Message = $"Brand {updateBrandDTO.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateBrandDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateBrandDTO.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id)
        {
            if (id == null)
                return new BaseResponse { Message = "Invalid Brand id", IsSucceeded = false };

            var Brand = await _unitOfWork.Brands.GetByIdAsync(id);
            if (Brand == null)
                return new BaseResponse { Message = "this Brand not found", IsSucceeded = false };
            try
            {
                await _unitOfWork.Brands.DeleteAsync(Brand);
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
