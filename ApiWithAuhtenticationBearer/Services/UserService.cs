
using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Enums;
using ApiWithAuhtenticationBearer.Exceptions;
using ApiWithAuhtenticationBearer.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace ApiWithAuhtenticationBearer.Services
{
    public class UserService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IMapper _mapper;
        public UserService(IPasswordHasher<User> passwordHasher, IMapper mapper)
        {
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        private static List<Role> Roles = new List<Role>()
        {
           new Role() { Id = 1, Name = EnumRole.user.ToString()},
           new Role() { Id = 2, Name = EnumRole.admin.ToString()},
           new Role() { Id = 3, Name = EnumRole.manager.ToString()}
        };

        private static List<User> Users = new List<User>()
        {
            new User(1, "mario@example.com" , "Mario", "Bros", DateTime.Now, "polish","",1)       
        };

        public List<User> GetAll()
        {
            var user = Users.Any(u => u.Id == 2);
            if (!user)
            {
                var newUser = new User()//TODO to zmienic za kazdym getem tworzy usera!!
                {
                    Id = 2,
                    Email = "user@example.com",
                    FirstName = "Mariusz",
                    LastName = "Malec",
                    DateOfBirth = DateTime.Now,
                    Nationality = "polish",
                    RoleId = 1
                };
                var hashedPassword = _passwordHasher.HashPassword(newUser, "user");
                newUser.PasswordHash = hashedPassword;
                Create(newUser);
            }

            user = Users.Any(u => u.Id == 3);
            if (!user)
            {
                var newUser = new User()//TODO to zmienic za kazdym getem tworzy usera!!
                {
                    Id = 3,
                    Email = "admin@example.com",
                    FirstName = "Mariusz",
                    LastName = "Malec",
                    DateOfBirth = DateTime.Now,
                    Nationality = "polish",
                    RoleId = 2
                };
                var hashedPassword = _passwordHasher.HashPassword(newUser, "admin");
                newUser.PasswordHash = hashedPassword;
                Create(newUser);
            }

            return Users.ToList();
        }
        public List<Role> GetAllRoles()
        {
            return Roles;
        }

        public UserDto GetById(int id)
        {
            var user = GetAll()
                .FirstOrDefault(u => u.Id == id);

            if (user is null)
                throw new NotFoundException("User not found");

            var result = _mapper.Map<UserDto>(user);
            return result;
        }

        public void Delete(int id)
        {
            //var user = GetById(id);
            //Users.Remove(user);
            throw new NotImplementedException();
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
