using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Enums;

namespace ApiWithAuhtenticationBearer.Context
{
    public class SeedData//TODO nie uzywane
    {
        private static List<Role> Roles = new List<Role>();
        public static void SeedRole()
        {
            Roles.Add
            (
                new Role()
                {
                    Id = 1,
                    Name = EnumRole.user.ToString()
                }
            );
            Roles.Add
            (
                new Role()
                {
                    Id = 2,
                    Name = EnumRole.admin.ToString()
                }
            );
            Roles.Add
            (
                new Role()
                {
                    Id = 3,
                    Name = EnumRole.manager.ToString()
                }
            );
        }
    }
}
