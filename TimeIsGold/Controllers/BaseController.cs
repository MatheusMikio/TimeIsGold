using Domain.Entities;
using Domain.Ports.Base;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TEntity, TService> : ControllerBase
        where TEntity : class
        where TService : IBaseService
    {
        protected readonly TService _service;

        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int page = 1, [FromQuery] int size = 12, [FromQuery] string q = null)
        {
            if (page < 1 || size < 1) return BadRequest("'Page' e 'Size' devem ser maiores que 0.");

            return Ok(_service.GetAll<TEntity>(page, size, q));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            if (id < 1) return BadRequest("id deve ser maior que 0.");

            TEntity entity = _service.GetById<TEntity>(id);

            if (entity == null) return NotFound("Não encontrado.");

            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            bool success = _service.Delete<TEntity>(id);

            if (!success) return NotFound("Não encontrado.");

            return NoContent();
        }
    }
}