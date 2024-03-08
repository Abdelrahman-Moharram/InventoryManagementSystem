using AutoMapper;
using InventoryManagementSystem.Domain.DTOs;
using InventoryManagementSystem.Domain.DTOs.Order;
using InventoryManagementSystem.Domain.Models;


namespace InventoryManagementSystem.Infrastructure.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, GetOrderDTO>()
                .ForMember(dest => 
                dest.ProductItems, opt => 
                    opt.MapFrom(src=>
                        src.ProductItems.Select(i=>
                            new OrderProductItemList { ProductItemId=i.Id}
                        ).ToList()
                       )
                    ).ReverseMap();
        }
    }
}
