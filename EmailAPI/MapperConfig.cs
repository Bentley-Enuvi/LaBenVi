using AutoMapper;
using EmailAPI.Models;
using EmailAPI.Models.DTOs;

namespace EmailAPI
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<AppUser, AppUserDto>();
            CreateMap<AppUserDto, AppUser>();
        }
    }
}
