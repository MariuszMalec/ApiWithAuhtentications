using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Enums;
using ApiWithAuhtenticationBearer.Services;

namespace ApiWithAuhtenticationBearer.Context
{
    public class UserSeeder
    {
        private readonly UserService _userService;
        private static IEnumerable<Role> Roles = new List<Role>();

        public UserSeeder(UserService userService)
        {
            _userService = userService;
        }

        public void Seed()
        {
            if (_userService.GetAll().Count() > 0)
            {
                if (_userService.GetAllRoles().Any())
                {
                    Roles = GetRoles();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name =  EnumRole.user.ToString()
                },
                new Role()
                {
                Name =  EnumRole.manager.ToString()
            },
                new Role()
                {
                    Name =  EnumRole.admin.ToString()
                },
            };
            return roles;
        }
    }
}
