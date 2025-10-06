using Application.DTOs.Scheduling;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports;
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
