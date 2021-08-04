using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;


namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseRestaurantController<Users, UserRepository>
    {
        public UsersController(UserRepository repository) : base(repository)
        {
        }

        [Authorize]
        public override async Task<IActionResult> Put(int id, Users user)
        {
            return await base.Put(id, user);
        }

        [Authorize]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }

        [HttpPost("login")]
        public  IActionResult loginUser(Users user)
        {
            if (user.Email == null || !user.Email.Contains("@") || user.Password == null || user.Password.Length < 6) 
            {
                return BadRequest();
            }

            var result = repository.login(user.Email, user.Password);

            if (result == null) 
            {
                return NotFound();
            }

            return Ok(result);
        }

        public override async Task<ActionResult<Users>> Post(Users user)
        {
            if (user.Email == null || !user.Email.Contains("@") || user.Password == null || user.Password.Length < 6)
            {
                return BadRequest();
            }

            user.Password = BC.HashPassword(user.Password);

            return await base.Post(user);
        }

        [Authorize]
        public override async Task<ActionResult<Users>> GetById(int id) 
        {
            return await base.GetById(id);
        }

    }
}
