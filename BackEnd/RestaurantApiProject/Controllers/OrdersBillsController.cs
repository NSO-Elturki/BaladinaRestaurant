using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Contracts;
using RestaurantApiProject.Models;
using RestaurantApiProject.Repositories;
using System.Threading.Tasks;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersBillsController : ControllerBase
    {
        IOrderBillRepository repository;
        public OrdersBillsController(IOrderBillRepository repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public  async Task<IActionResult> Post(OrdersBills obj)

        {
            if (obj == null)
            {
                return BadRequest();
            }

            await repository.create(obj);
            return Ok();

        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await this.repository.getAll());
        }





    }
}
