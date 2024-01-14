using AutoMapper;
using LaBenVi.Products.Models;
using LaBenVi.Products.Models.DTOs;

namespace LaBenVi.Products
{
    public class MappingConfig
    {
        public static MapperConfiguration MapsReg()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
                //config.CreateMap<UpdateCouponDto, Coupon>();
            });
            return mappingConfig;
        }
    }
}
