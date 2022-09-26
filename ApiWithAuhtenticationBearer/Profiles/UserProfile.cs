using ApiWithAuhtenticationBearer.Entities;
using AutoMapper;

namespace ApiWithAuhtenticationBearer.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, Models.UserDto>()
            //.ForMember(d => d.UserRole, o => o.MapFrom(s => $"admin"))
            ;
        }
    }
}
