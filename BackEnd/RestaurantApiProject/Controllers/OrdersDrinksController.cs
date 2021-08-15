using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersDrinksController : BaseRestaurantController<OrdersDrinks, OrderDrinksRepository>
    {
        public OrdersDrinksController(OrderDrinksRepository repository) : base(repository)
        {
        }
    }
}
