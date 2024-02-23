using AutoMapper;
using InventoryManagementSystem.Domain.DTOs;
using InventoryManagementSystem.Domain.DTOs.Brand;
using InventoryManagementSystem.Domain.Models;


namespace InventoryManagementSystem.Infrastructure.Mappers
{
    public class BrandProfile : Profile
    {
        public BrandProfile() 
        {
            CreateMap<Brand, GetBrandDTO>()
                .ForMember(dest => dest.BrandProducts,
                    opt => opt.MapFrom(src =>
                        src.Products.Select(
                            prodbase => new SimpleModule { Id = prodbase.Id, Name = prodbase.Name }
                            ).ToList()
                        )
                    ).ReverseMap();

            CreateMap<AddBrandDTO, Brand>().ReverseMap();
            CreateMap<UpdateBrandDTO, Brand>().ReverseMap();
        }
    }
}
