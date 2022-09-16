using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Exceptions;
using ApiWithAuhtenticationBearer.Interfaces;
using ApiWithAuhtenticationBearer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiWithAuhtenticationBearer.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserService _userService;
        private readonly AutenticationSettings _authenticationSettings;

        public AccountService(IPasswordHasher<User> passwordHasher, UserService userService, AutenticationSettings authenticationSettings)
        {
            _passwordHasher = passwordHasher;
            _userService = userService;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt([FromBody] LoginDto dto)
        {
            var user = _userService.GetAll().FirstOrDefault(u => u.Email == dto.Email);
            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");//https://youtu.be/exKLvxaPI6Y?t=3597
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            //take role for user
            var roleId = _userService.GetAll().Where(u=>u.Email == dto.Email)
                .Select(r=>r.RoleId).FirstOrDefault();

            var role = _userService.GetAllRoles().Where(r => r.Id == roleId).FirstOrDefault();

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, $"{role.Name}"),//TODO to czyta autorize!
                new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")),
            };

            if (!string.IsNullOrEmpty(user.Nationality))
            {
                claims.Add(
                    new Claim("Nationality", user.Nationality)
                    );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser([FromBody] RegisterUserDto dto)
        {
            var user = _userService.GetAll().ToList().Any(u => u.Email == dto.Email);
            if (user)
            {
                throw new BadRequestException($"Email {dto.Email} exist yet!");
            }

            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                RoleId = dto.RoleId
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = hashedPassword;
            _userService.Create(newUser);
        }
    }
}
