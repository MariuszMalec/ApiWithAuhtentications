
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
           new Role() { Id = 2, Name = EnumRole.manager.ToString()},
           new Role() { Id = 3, Name = EnumRole.admin.ToString()}
        };

        private static List<User> Users = new List<User>() { };

        public List<User> GetAll()
        {
            var user = Users.Any(u => u.Id == 1);
            if (!user)
            {
                var newUser = new User()//TODO to zmienic za kazdym getem tworzy usera!!
                {
                    Email = "jordan@example.com",
                    FirstName = "Michel",
                    LastName = "Jordan",
                    DateOfBirth = new DateTime(2013,5,1),
                    Nationality = "usa",
                    RoleId = 2,
                    Role = new Role { Id = 2, Name = EnumRole.manager.ToString() }
                };
                var hashedPassword = _passwordHasher.HashPassword(newUser, "jordan");
                newUser.PasswordHash = hashedPassword;
                Create(newUser);
            }

            user = Users.Any(u => u.Id == 2);
            if (!user)
            {
                var newUser = new User()//TODO to zmienic za kazdym getem tworzy usera!!
                {
                    Email = "user@example.com",
                    FirstName = "User",
                    LastName = "Userek",
                    DateOfBirth = new DateTime(2016, 5, 1),
                    Nationality = "usa",
                    RoleId = 1,
                    Role = new Role { Id = 1, Name = EnumRole.user.ToString() }
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
                    Email = "mariusz@example.com",
                    FirstName = "Mariusz",
                    LastName = "Malec",
                    DateOfBirth = new DateTime(2014, 5, 1),
                    Nationality = "polish",
                    RoleId = 3,
                    Role = new Role { Id = 3, Name = EnumRole.admin.ToString() }
                };
                var hashedPassword = _passwordHasher.HashPassword(newUser, "admin");
                newUser.PasswordHash = hashedPassword;
                Create(newUser);
            }
            user = Users.Any(u => u.Id == 4);
            if (!user)
            {
                var newUser = new User()//TODO to zmienic za kazdym getem tworzy usera!!
                {
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "Adminek",
                    DateOfBirth = new DateTime(2014, 5, 1),
                    Nationality = "polish",
                    RoleId = 3,
                    Role = new Role { Id = 3, Name = EnumRole.admin.ToString() }
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

        public int Create(User model)
        {
            model.Id = GetNextId();
            Users.Add(model);
            return model.Id;
        }

        public int GetNextId()
        {
            if (!Users.Any())
                return 1;
            return (Users?.Max(m => m.Id) ?? 0) + 1;
        }

        public void Delete(int id)
        {
            //_logger.LogError($"Restaurant with id: {id} DELETE action invoked");

            //var user = GetAll() 
            //    .FirstOrDefault(r => r.Id == id);

            //if (user is null)
            //    throw new NotFoundException("Restaurant not found");

            //var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant,
            //    new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            //if (!authorizationResult.Succeeded)
            //{
            //    throw new ForbidException();
            //}

            //_dbContext.Restaurants.Remove(restaurant);
            //_dbContext.SaveChanges();

        }
    }
}
