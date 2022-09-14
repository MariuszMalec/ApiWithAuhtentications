using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Enums;

namespace ApiWithAuhtenticationBearer.Context
{
    public class SeedData//TODO nie uzywane
    {
        private static List<Role> Roles = new List<Role>();
        public static void SeedRole()
        {
            Roles.Add(
                new Role()
                {
                    Id = 1,
                    Name = EnumRole.user.ToString()
                });
        }
    }
}
