using AutoMapper;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.Models;


namespace InventoryManagementSystem.Infrastructure.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDTO>()
                .ForMember(dest=>dest.Colors, 
                    opt => opt.MapFrom(src=>src.ProductItems.Where(ii=>ii.ProductId == src.Id).Select(i=>i.Color).ToList()
                    )
                );

            CreateMap<Product, AddProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
        }
    }
}
