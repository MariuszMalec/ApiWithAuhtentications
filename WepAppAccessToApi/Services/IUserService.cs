using WepAppAccessToApi.Models;

namespace WepAppAccessToApi.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAll();

        Task<User> GetUserById(int id);
    }
}
