using AutoMapper;
using LaBenVi_CouponAPI.Models;
using LaBenVi_CouponAPI.Models.DTOs;

namespace LaBenVi_CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration MapsReg()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
                //config.CreateMap<UpdateCouponDto, Coupon>();
            });
            return mappingConfig;
        }
    }
}
