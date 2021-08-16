using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;
using System.Threading.Tasks;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersFoodController : BaseOrderController<OrdersFood, OrderFoodRepository>
    {
        public OrdersFoodController(OrderFoodRepository repository) : base(repository)
        {
        }

        public override async Task<IActionResult> Post(OrdersFood ordersFood)
        {
            if (ordersFood == null || ordersFood.FoodId <= 0 )
            {
                return BadRequest();
            }

            return await base.Post(ordersFood);
        }


    }
}


