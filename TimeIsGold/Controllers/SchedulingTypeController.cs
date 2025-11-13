using Application.DTOs.SchedulingType;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
using Domain.Ports.SchedulingType;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingTypeController : BaseController<SchedulingTypeDTOOutput, ISchedulingTypeService>
    {
        public SchedulingTypeController(ISchedulingTypeService service) : base(service)
        {
        }

        [HttpPost]
        public IActionResult Create([FromBody] SchedulingTypeDTO scheduling)
        {
            if (_service.Create(scheduling, out List<ErrorMessage> errors)) return CreatedAtAction(nameof(Create), scheduling);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] SchedulingTypeDTOUpdate scheduling)
        {
            _service.Update(scheduling, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }
    }
}
