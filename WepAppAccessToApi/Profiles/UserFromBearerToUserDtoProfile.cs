using AutoMapper;
using WepAppAccessToApi.Models;

namespace WepAppAccessToApi.Profiles
{
    public class UserFromBearerToUserDtoProfile : Profile
    {
        public UserFromBearerToUserDtoProfile()
        {
            CreateMap<UserGet, UserDto>()
                .ForMember(d => d.PasswordHash, o => o.Ignore());
            ;
        }
    }
}
