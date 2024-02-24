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
                .ForMember(dest=>dest.Colors, opt => opt.MapFrom(src=>src.ProductItems.Where(ii=>ii.ProductId == src.Id).Select(i=>i.Color).ToList()))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.Inventories.Select(i => i.ProductsInventory).ToList()))
                .ForMember(dest => dest.Amount,opt => opt.MapFrom(src=>src.ProductItems.Count()))
                .ForMember(dest => dest.BrandName,opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<AddProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
        }
    }
}
