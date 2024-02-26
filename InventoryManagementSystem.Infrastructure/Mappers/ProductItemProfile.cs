using AutoMapper;
using InventoryManagementSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Mappers
{
    public class ProductItemProfile : Profile
    {
        public ProductItemProfile() 
        {
            CreateMap<ProductItem,  ProductItemProfile>().ReverseMap();
        }
    }
}
