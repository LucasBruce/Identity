using AutoMapper;
using WebAPI.Domain;
using WebAPI.Identity.Dto;

namespace WebAPI.Identity.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
        }
    }
}
