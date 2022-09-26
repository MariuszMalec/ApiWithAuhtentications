using ApiWithAuhtenticationBearer.Entities;
using ApiWithAuhtenticationBearer.Models;
using ApiWithAuhtenticationBearer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiWithAuhtenticationBearer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>

        [HttpGet]
        //[Authorize(Roles = "admin,manager")]
        public IActionResult Get()
        {
            var users = _userService.GetAll();
            if (!users.Any())
                return BadRequest($"Brak uzytkowników!");
            return Ok(users);
        }
        
        [HttpGet("Roles")]
        [AllowAnonymous]
        public IActionResult GetRolles()
        {
            var roles = _userService.GetAllRoles();
            if (!roles.Any())
                return BadRequest($"Brak rol!");
            return Ok(roles);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        //[Authorize(Policy = "HasNationality")]
        [Authorize(Policy = "Atleast20")]
        public ActionResult<User> Get([FromRoute] int id)
        {
            var user = _userService.GetById(id);

            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateUser([FromBody] User model)
        {
            var id = _userService.Create(model);

            return Created($"/api/user/{id}", null);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
