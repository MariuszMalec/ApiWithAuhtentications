using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Models;
using AutoMapper;

namespace ApiWithAuhtenticationBearer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
            //.ForMember(d => d.UserRole, o => o.MapFrom(s => $"admin"))
            ;
        }
    }
}
