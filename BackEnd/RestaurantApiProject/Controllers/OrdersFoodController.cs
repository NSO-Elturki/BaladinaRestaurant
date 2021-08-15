using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersFoodController : BaseRestaurantController<OrdersFood, OrderFoodRepository>
    {
        public OrdersFoodController(OrderFoodRepository repository) : base(repository)
        {
        }
    }
}


