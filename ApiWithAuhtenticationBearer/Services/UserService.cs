
using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Enums;
using Microsoft.AspNetCore.Identity;

namespace ApiWithAuhtenticationBearer.Services
{
    public class UserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(IPasswordHasher<User> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        private static List<Role> Roles = new List<Role>()
        {
           new Role() { Id = 1, Name = EnumRole.user.ToString()}
        };

        private static List<User> Users = new List<User>()
        {
            new User(1, "mario@example.com" , "Mario", "Bros", DateTime.Now, "polish","",1)       
        };

        public List<User> GetAll()
        {
            var newUser = new User()//TODO to zmienic za kazdym getem tworzy usera!!
            {
                Id = 2,
                Email = "test@example.com",
                FirstName = "Mariusz",
                LastName = "Malec",
                DateOfBirth = DateTime.Now,
                Nationality = "polish",
                RoleId = 1
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, "12345");
            newUser.PasswordHash = hashedPassword;
            Create(newUser);

            return Users.ToList();
        }
        public List<Role> GetAllRoles()
        {
            return Roles;
        }
        public User GetById(int id)
        {
            //Users = GetAll();
            return Users.SingleOrDefault(u => u.Id == id);
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            Users.Remove(user);
        }

        public void Create(User model)
        {
            model.Id = GetNextId();
            Users.Add(model);
        }
        public int GetNextId()
        {
            if (!Users.Any())
                return 0;
            return (Users?.Max(m => m.Id) ?? 0) + 1;
        }
    }
}
