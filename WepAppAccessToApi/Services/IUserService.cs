using WepAppAccessToApi.Models;

namespace WepAppAccessToApi.Services
{
    public interface IUserService
    {
        Task<bool> Edit(int id, User model);
        Task<List<User>> GetAll();

        Task<bool> Delete(int id, User model);
        Task<User> GetUserById(int id);
        Task<bool> Insert(User model);
    }
}
