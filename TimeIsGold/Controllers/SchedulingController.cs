using Application.DTOs.Scheduling;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Scheduling;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingController : BaseController<SchedulingDTOOutput, ISchedulingService>
    {
        public SchedulingController(ISchedulingService service) : base(service)
        {
        }

        [HttpGet("GetTodaySchedulings/{id}")]
        public IActionResult GetTodaySchedulings(long id)
        {
            int schedulings = _service.GetTodaySchedulings(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(schedulings);

            return BadRequest(errors);
        }

        [HttpGet("GetWeekSchedulings/{id}")]
        public IActionResult GetWeekSchedulings(long id)
        {
            List<SchedulingDTOOutput> schedulings = _service.GetWeekSchedulings(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(schedulings);

            return BadRequest(errors);
        }

        [HttpGet("GetMonthSchedulings/{id}")]
        public IActionResult GetMonthSchedulings(long id)
        {
            List<SchedulingDTOOutput> schedulings = _service.GetMonthSchedulings(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(schedulings);

            return BadRequest(errors);
        }

        [HttpGet("GetPendentsSchedulings/{id}")]
        public IActionResult GetPendentsSchedulings(long id)
        {
            int schedulings = _service.GetPendentsSchedulings(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(schedulings);

            return BadRequest(errors);
        }

        [HttpPost]
        public IActionResult Create([FromBody]SchedulingDTO schedulingDTO)
        {
            if (_service.Create(schedulingDTO, out List<ErrorMessage> errors)) return CreatedAtAction(nameof(Create), schedulingDTO);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] SchedulingDTOUpdate schedulingDTOUpdate)
        {
            _service.Update(schedulingDTOUpdate, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }
    }
}
