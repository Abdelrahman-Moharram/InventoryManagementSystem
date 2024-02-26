using AutoMapper;
using InventoryManagementSystem.Domain.DTOs;
using InventoryManagementSystem.Domain.DTOs.Inventory;
using InventoryManagementSystem.Domain.Models;


namespace InventoryManagementSystem.Infrastructure.Mappers
{
    public class InventoryProfile : Profile
    {
        public InventoryProfile() 
        {
            CreateMap<Inventory, GetInventoryDTO>()
                .ForMember(dest=>
                    dest.Products, option=>
                        option.MapFrom(src=>
                            src.Products.Select(p=>new SimpleModule { Id=p.Id, Name=p.Name}).ToList()
                            )
                      ).ReverseMap();

            CreateMap<AddInventoryDTO, Inventory>();
            CreateMap<UpdateInventoryDTO, Inventory>();
        }
    }
}
