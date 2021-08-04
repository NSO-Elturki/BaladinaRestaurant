using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Services;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseRestaurantController<Orders, OrderRepository>
    {
        public OrdersController(OrderRepository repository) : base(repository)
        {
        }
    }
}

