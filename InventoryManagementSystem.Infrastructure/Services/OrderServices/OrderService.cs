using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Order;
using InventoryManagementSystem.Domain.DTOs.Response;
using InventoryManagementSystem.Domain.Models;
using InventoryManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;



namespace InventoryManagementSystem.Infrastructure.Services.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<OrderService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<IEnumerable<GetOrderDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<GetOrderDTO>>(await _unitOfWork.Orders.GetAllAsync());
        }

        public async Task<IEnumerable<GetOrderDTO>> GetUserOrders(string UserId)
        {
            var order = _unitOfWork.Orders.FindAllAsync(i=>i.CustomerId == UserId ,includes: new[] { "ProductItems" }, IgnoreGlobalFilters: true)
                .Result
                .ToList();
            var orderDTO =
                _mapper.Map<IEnumerable<GetOrderDTO>>(order).ToList();
            foreach (var item in orderDTO)
            {
                foreach (var i in item?.ProductItems?.ToList())
                {
                    var product = _unitOfWork.ProductItems.Find(pi => pi.Id == i.ProductItemId, includes: new[] { "Product" }, IgnoreGlobalFilters: true).Result.Product;
                    i.ProductName = product.Name;
                    i.ProductId = product.Id;
                }
            }
            return orderDTO;
        }

        public async Task<IEnumerable<GetOrderDTO>> GetAllWithBaseIncludes()
        {
            var order = _unitOfWork.Orders.GetAllAsync(new[] { "ProductItems" }, IgnoreGlobalFilters:true)
                .Result
                .ToList();
            var orderDTO = 
                _mapper.Map<IEnumerable<GetOrderDTO>>(order).ToList();
            foreach (var item in orderDTO)
            {
                foreach (var i in item?.ProductItems?.ToList())
                {
                    var product = _unitOfWork.ProductItems.Find(pi => pi.Id == i.ProductItemId, includes: new[] { "Product" }, IgnoreGlobalFilters:true).Result.Product;
                    i.ProductName = product.Name;
                    i.ProductId = product.Id;
                }
            }
            return orderDTO;
        }
        public async Task<GetOrderDTO> GetById(string id) =>
            _mapper.Map<GetOrderDTO>(await _unitOfWork.Orders.GetByIdAsync(id));

        public async Task<BaseResponse> AddNew(IEnumerable<string> productItemsIds, string CreatedBy)
        {
            if (string.IsNullOrEmpty(CreatedBy))
                return new BaseResponse { Message = "Can't assign Transacation To user, user id is empty", IsSucceeded = false };

            if (productItemsIds == null || productItemsIds.Count() == 0)
                return new BaseResponse { Message = "Invalid Order Product List is Empty", IsSucceeded = false };

            Order newOrder = new();
            newOrder.CreatedBy = CreatedBy;
            try
            {
                foreach (var item in productItemsIds.ToList())
                {
                    var ProductItem = await _unitOfWork.ProductItems.Find(i=>i.Id == item && !i.IsSelled);

                    if (ProductItem is null)
                        return new BaseResponse { Message =$"Product with id={item} not found or Selled", IsSucceeded=false};
                    
                    var product = await _unitOfWork.Products.GetByIdAsync(ProductItem.ProductId);
                    var productInv = await _unitOfWork.ProductsInventories.GetByIdAsync(ProductItem.ProductsInventoryId);


                    product.Amount = await _unitOfWork.ProductItems.CountAsync(i => i.ProductId == product.Id);
                    productInv.Amount = await _unitOfWork.ProductItems.CountAsync(i => i.ProductId == product.Id && i.ProductsInventoryId == productInv.Id);

                    ProductItem.IsSelled = true;
                    ProductItem.OrderId = newOrder.Id;

                    await _unitOfWork.ProductItems.UpdateAsync(ProductItem);
                    newOrder.TotalAmount ++;
                    newOrder.TotalPrice += product.Price;
                }
            newOrder.CreatedBy = CreatedBy;
            newOrder.CustomerId = CreatedBy;
            await _unitOfWork.Orders.AddAsync(newOrder);
            await _unitOfWork.Save();
            return new BaseResponse { Message = $"Order placed Successfully", IsSucceeded = true };
            
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while adding the order placed by {CreatedBy}", ex);
                return new BaseResponse { Message = "Something went wrong while placing the Order", IsSucceeded = false };
            }

        }

    }
}
