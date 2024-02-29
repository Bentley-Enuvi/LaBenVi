using AutoMapper;
using LaBenVi_OrderAPI.Models;
using LaBenVi_OrderAPI.Models.DTOs;

namespace LaBenVi_OrderAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<OrderHeaderDto, CartHeaderDto>()
                .ForMember(destination => destination.CartTotal, u => u.MapFrom(src => src.OrderTotal)).ReverseMap();

                config.CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(destination => destination.ProductName, u => u.MapFrom(src => src.Product.Name))
                .ForMember(destination => destination.Price, u => u.MapFrom(src => src.Product.Price));

                config.CreateMap<OrderDetailsDto, CartDetailsDto>();

                config.CreateMap<OrderHeader, OrderHeaderDto>().ReverseMap();
                config.CreateMap<OrderDetailsDto, OrderDetails>().ReverseMap();

            });
            return mappingConfig;
        }
    }
}
