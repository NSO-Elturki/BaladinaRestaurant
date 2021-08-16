using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Contracts;
using System.Threading.Tasks;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseOrderController<T, repo> : ControllerBase
        where T : class, IEntity
        where repo : IBaseOrderRepository<T>
    {
        public readonly repo repository;

        public BaseOrderController(repo trepo)
        {
            this.repository = trepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await this.repository.getAll());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T obj)
        {
            if (obj == null)
            {
                return BadRequest();
            }

            if (repository.isExists(obj.Id) == true)
            {
                return BadRequest();
            }

            await repository.create(obj);
            return Ok();

        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Id can not be 0 or less!");
            }

            var i = await repository.removeById(id);

            if (i == false)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
