
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodsController : BaseRestaurantController<Food,FoodRepository>
    {
        public FoodsController(FoodRepository repository):base(repository)
        {
        }

   //   [Authorize]
        public override async Task<ActionResult<Food>> Post(Food food) 
        {
            if (food == null || food.FoodDescription == null || food.FoodPrice <=0  || food.FoodType == null || food.FoodName == null)
            {
                return BadRequest();
            }

            return await base.Post(food);
        }

    //   [Authorize]
        public override async Task<IActionResult> Put(int id, Food food)
        {
            return await base.Put(id,food);
        }

   //     [Authorize]
        public override async Task<IActionResult> Delete(int id)
        {
            return await base.Delete(id);
        }
    }
}
