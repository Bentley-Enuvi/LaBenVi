using AutoMapper;
using LaBenVi_AuthService.Models.Dto;
using LaBenVi_AuthService.Models;


namespace LaBenVi_AuthService
{
    public class MappingConfig
    {
        public static MapperConfiguration MapsReg()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<AppUserDto, AppUser>().ReverseMap(); 

            });
            return mappingConfig;
        }
    }
}
