using ApiWithAuhtenticationBearer.Models;

namespace ApiWithAuhtenticationBearer.Interfaces
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
    }
}
