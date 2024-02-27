﻿using AutoMapper;
using InventoryManagementSystem.Domain.DTOs;
using InventoryManagementSystem.Domain.DTOs.Product;
using InventoryManagementSystem.Domain.DTOs.ProductItems;
using InventoryManagementSystem.Domain.Models;


namespace InventoryManagementSystem.Infrastructure.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, GetProductDTO>()
                .ForMember(dest=>dest.Colors, opt => opt.MapFrom(src=>src.ProductItems.Where(ii=>ii.ProductId == src.Id).Select(i=>i.Color).ToList()))
                .ForMember(dest => dest.ProductsInventory, opt => opt.MapFrom(src => src.Inventories.Select(i => new SimpleModule { Id=i.Id, Name=i.Name}).ToList()))
                .ForMember(dest => dest.Amount,opt => opt.MapFrom(src=>src.ProductItems.Count()))
                .ForMember(dest => dest.BrandName,opt => opt.MapFrom(src => src.Brand.Name))
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category.Name))

                .ForMember(dest => dest.ProductItems, opt => opt.MapFrom(src => 
                    src.ProductItems.Select(i=> 
                        new GetProductItemIncludedDTO 
                        { 
                            Color = i.Color, 
                            InventoryId = i.InventoryId, 
                            IsSelled=i.IsSelled, 
                            OrderId=i.OrderId, 
                            SerialNo=i.SerialNo
                        })
                    ));

            CreateMap<AddProductDTO, Product>().ReverseMap();
            CreateMap<UpdateProductDTO, Product>().ReverseMap();
        }
    }
}
