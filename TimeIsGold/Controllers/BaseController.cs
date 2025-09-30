using Domain.Entities;
using Domain.Ports;
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

            return string.IsNullOrEmpty(q) ? Ok(_service.GetAll<TEntity>(page, size)) : Ok(_service.Get<TEntity>(q));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            if (id < 1) return BadRequest("id deve ser maior que 0.");

            TEntity entity = _service.GetById<TEntity>(id);

            if (entity == null) return NotFound("Não encontrado.");

            return Ok(entity);
        }


        [HttpPut]
        public IActionResult Update([FromBody] TEntity entity)
        {
            _service.Update<TEntity>(entity, out List<ErrorMessage> erros);

            if (erros.Count == 0) return NoContent();
            
            return BadRequest(erros);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            bool sucess = _service.Delete<TEntity>(id);

            if (!sucess) return NotFound("Não encontrado.");

            return NoContent();
        }
    }
}
