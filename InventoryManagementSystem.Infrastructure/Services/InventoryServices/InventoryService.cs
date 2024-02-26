using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Inventory;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;


namespace InventoryManagementSystem.Infrastructure.Services.InventoryServices
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<InventoryService> _logger;
        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<InventoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<GetInventoryDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<GetInventoryDTO>>(await _unitOfWork.Inventories.GetAllAsync());
        }

        public async Task<IEnumerable<GetInventoryDTO>> GetAllWithBaseIncludes()
        {
            return _mapper.Map<IEnumerable<GetInventoryDTO>>(await _unitOfWork.Inventories.GetAllAsync(new[] { "Products", "ProductsInventory", "ProductItems" }));
        }
        public async Task<GetInventoryDTO> GetById(string id)
        {
            return _mapper.Map<GetInventoryDTO>(await _unitOfWork.Inventories.GetByIdAsync(id));
        }
        public async Task<IEnumerable<GetInventoryDTO>> Search(string SearchQuery)
        {
            return _mapper.Map<IEnumerable<GetInventoryDTO>>(
                await _unitOfWork.Inventories.FindAllAsync(
                        i => i.Name.Contains(SearchQuery) || i.Id.Contains(SearchQuery)
                    ));

        }
        public async Task<BaseResponse> AddNew(AddInventoryDTO newInventoryDTO, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (newInventoryDTO.Name == null)
                return new BaseResponse { Message = "Invalid Inventory Name", IsSucceeded = false };

            else if (await _unitOfWork.Inventories.Find(i => i.Name == newInventoryDTO.Name) != null)
                return new BaseResponse { Message = $"Inventory with {newInventoryDTO.Name} Name Already Exisits", IsSucceeded = false };
            try
            {
                var newInventory = _mapper.Map<Inventory>(newInventoryDTO);
                newInventory.CreatedBy = CreatedBy;
                await _unitOfWork.Inventories.AddAsync(newInventory);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Inventory {newInventoryDTO.Name} added Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding {newInventoryDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while adding {newInventoryDTO.Name}", IsSucceeded = false };
            }

        }
        public async Task<BaseResponse> Update(UpdateInventoryDTO updateInventoryDTO, string UpdatedBy)
        {
            if (string.IsNullOrEmpty(UpdatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (updateInventoryDTO.Name == null)
                return new BaseResponse { Message = "Invalid Inventory Name", IsSucceeded = false };

            else if (await _unitOfWork.Inventories.Find(i => i.Id == updateInventoryDTO.Id) == null)
                return new BaseResponse { Message = $"Inventory Doesn't Exisit", IsSucceeded = false };


            try
            {
                var entity = _mapper.Map<Inventory>(updateInventoryDTO);
                entity.UpdatedBy = UpdatedBy;
                await _unitOfWork.Inventories.UpdateAsync(entity);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Inventory {updateInventoryDTO.Name} Updated Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while updating {updateInventoryDTO.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while updating {updateInventoryDTO.Name}", IsSucceeded = false };
            }
        }
        public async Task<BaseResponse> Delete(string id, string DeletedBy)
        {
            if (string.IsNullOrEmpty(DeletedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };
            if (id == null)
                return new BaseResponse { Message = "Invalid Inventory id", IsSucceeded = false };

            var Inventory = await _unitOfWork.Inventories.GetByIdAsync(id);
            if (Inventory == null)
                return new BaseResponse { Message = "this Inventory not found", IsSucceeded = false };
            try
            {
                Inventory.DeletedBy = DeletedBy;
                await _unitOfWork.Inventories.DeleteAsync(Inventory);
                await _unitOfWork.Save();
                return new BaseResponse { Message = $"Inventory {Inventory.Name} Deleted Successfully", IsSucceeded = true };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while Deleting {Inventory.Name}", ex);
                return new BaseResponse { Message = $"Something went wrong while Deleting {Inventory.Name}", IsSucceeded = false };
            }
        }
    }
}
