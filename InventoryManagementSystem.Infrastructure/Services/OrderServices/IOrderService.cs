using InventoryManagementSystem.Domain.DTOs.Order;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Services.OrderServices
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrderDTO>> GetAll();
        Task<IEnumerable<GetOrderDTO>> GetAllWithBaseIncludes();
        Task<IEnumerable<GetOrderDTO>> GetUserOrders(string UserId);
        Task<GetOrderDTO> GetById(string id);
        Task<BaseResponse> AddNew(IEnumerable<string> productItemsIds, string CreatedBy);

    }
}
