using AutoMapper;
using InventoryManagementSystem.Domain.DTOs;
using InventoryManagementSystem.Domain.DTOs.Category;
using InventoryManagementSystem.Domain.Models;


namespace InventoryManagementSystem.Infrastructure.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, GetCategoryDTO>()
                .ForMember(dest => dest.CategoryProducts, 
                    opt => opt.MapFrom(src => 
                        src.Products.Select(
                            prodbase=>new SimpleModule { Id= prodbase.Id, Name = prodbase.Name}
                            ).ToList()
                        )
                    ).ReverseMap();

            CreateMap<AddCategoryDTO, Category>().ReverseMap();
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();

        }
    }
}
