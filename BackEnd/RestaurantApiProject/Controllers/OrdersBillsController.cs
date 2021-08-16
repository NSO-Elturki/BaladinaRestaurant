using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;
using System.Threading.Tasks;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersBillsController : BaseRestaurantController<OrdersBills, OrdersBillsRepository>
    {
        public OrdersBillsController(OrdersBillsRepository repository) : base(repository)
        {
        }

       
    }
}
