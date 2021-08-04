using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantApiProject.Contracts;
using System.Threading.Tasks;

namespace RestaurantApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseRestaurantController<T, repo> : ControllerBase
        where T : class, IEntity 
        where repo : IBaseRepository<T>
    {
        public readonly repo repository;

        public BaseRestaurantController(repo trepo) 
        {
            this.repository = trepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await this.repository.getAll());

        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<T>> GetById(int id)
        {
            var obj = await repository.getById(id);
            if (obj == null)
            {
                return NotFound();
            }
            return obj;
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(int id, T obj)
        {

            if (id <= 0)
            {
                return BadRequest("Id can not be 0 or less!");
            }

            if (obj == null)
            {
                return BadRequest();
            }

            if (id != obj.Id)
            {
                return BadRequest("The insert Id do not equal the Id of the object need to edit!");
            }

            var response = await this.repository.edit(obj);

            if (response == false) 
            {
                return NotFound();
            }

            return Ok();

        }


        [HttpPost]
        public virtual async Task<ActionResult<T>> Post(T obj)
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

            return CreatedAtAction(nameof(GetById), new { id = obj.Id }, obj);
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
