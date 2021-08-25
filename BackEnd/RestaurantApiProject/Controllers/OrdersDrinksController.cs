using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDrinksController : BaseOrderController<OrdersDrinks, OrderDrinksRepository>
    {
        public OrdersDrinksController(OrderDrinksRepository repository) : base(repository)
        {
        }

        public override async Task<IActionResult> Post(List<OrdersDrinks> ordersDrinks)
        {
            //if (ordersDrinks == null || ordersDrinks.DrinkId <= 0)
            //{
            //    return BadRequest();
            //}

            return await base.Post(ordersDrinks);
        }

    }
}
