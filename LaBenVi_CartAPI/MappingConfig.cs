using AutoMapper;
using LaBenVi_CartAPI.Models;
using LaBenVi_CartAPI.Models.DTOs;

namespace LaBenVi_CartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration MapsReg()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
