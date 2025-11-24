using Application.DTOs.Scheduling;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Scheduling;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeIsGold.Controllers.Base;

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
        [Authorize(Policy = "RequiredType")]
        public IActionResult GetTodaySchedulings(long id)
        {
            int schedulings = _service.GetTodaySchedulings(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(schedulings);

            return BadRequest(errors);
        }

        [HttpGet("GetPendentsSchedulings/{id}")]
        [Authorize(Policy = "RequiredType")]
        public IActionResult GetPendentsSchedulings(long id)
        {
            int schedulings = _service.GetPendentsSchedulings(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(schedulings);

            return BadRequest(errors);
        }

        [HttpGet("GetTodaySchedulingsStatusProfessional/{id}")]
        public IActionResult GetTodaySchedulingsProfessional(long id)
        {
            SchedulingStatisticsDTO statistics = _service.GetTodaySchedulingsProfessional(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(statistics);

            return BadRequest(errors);
        }

        [HttpGet("GetTodaySchedulingsProfessional/{id}")]
        public IActionResult GetSchedulingByDate(long id)
        {
            List<SchedulingDTOOutput> exists = _service.GetSchedulingToday(id, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(exists);

            return BadRequest(errors);
        }

        [HttpGet("GetSchedulingsByPeriod/{id}")]
        public IActionResult GetSchedulingsByPeriod(long id, [FromQuery] PeriodType periodType)
        {
            List<SchedulingDTOOutput> schedulings = _service.GetSchedulingsByPeriod(id, periodType, out List<ErrorMessage> errors);

            if (errors.Count == 0) return Ok(schedulings);

            return BadRequest(errors);
        }

        [HttpPost]
        public IActionResult Create([FromBody] SchedulingDTO schedulingDTO)
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
