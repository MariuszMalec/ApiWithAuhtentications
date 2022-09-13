using ApiWithAuhtenticationBearer.Interfaces;
using ApiWithAuhtenticationBearer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiWithAuhtenticationBearer.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        //TODO zrobic bez bazy rejestracje uproszczona
        //TODO dodac role najlepiej seed!!!!!
        //patrz https://github.com/jakubkozera/RestaurantAPI/blob/authorization-start/RestaurantAPI/Services/AccountService.cs
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok($"User with email {dto.Email} was register");
        }

        [HttpPost("login")]//TODO https://youtu.be/exKLvxaPI6Y?t=3382
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
