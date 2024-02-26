using InventoryManagementSystem.Domain.DTOs.Inventory;
using InventoryManagementSystem.Domain.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services.InventoryServices
{
    public interface IInventoryService
    {
        Task<IEnumerable<GetInventoryDTO>> GetAll();
        Task<IEnumerable<GetInventoryDTO>> GetAllWithBaseIncludes();
        Task<GetInventoryDTO> GetById(string id);
        Task<IEnumerable<GetInventoryDTO>> Search(string SearchQuery);
        Task<BaseResponse> AddNew(AddInventoryDTO newInventoryDTO, string CreatedBy);
        Task<BaseResponse> Update(UpdateInventoryDTO updateInventoryDTO, string UpdatedBy);
        Task<BaseResponse> Delete(string id, string DeletedBy);
    }
}
