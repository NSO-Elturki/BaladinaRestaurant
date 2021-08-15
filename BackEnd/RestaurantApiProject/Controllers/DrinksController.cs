using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;
using System.Threading.Tasks;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : BaseRestaurantController<Drinks, DrinksRepository>
    {
        public DrinksController(DrinksRepository repository) : base(repository)
        {
        }

       [Authorize]
        public override async Task<ActionResult<Drinks>> Post(Drinks drink)
        {
            if (drink == null || drink.DrinkPrice <= 0 || drink.DrinkType == null || drink.Quantity <= 0 || drink.DrinkName == null)
            {
                return BadRequest();
            }

            return await base.Post(drink);
        }

      [Authorize]
        public override async Task<IActionResult> Put(int id, Drinks drink)
        {
            return await base.Put(id, drink);
        }

       [Authorize]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
