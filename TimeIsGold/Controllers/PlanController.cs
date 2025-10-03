using Application.DTOs.Plan;
using Domain.DTOs.Plan;
using Domain.Entities;
using Domain.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : BaseController<PlanDTOOutput, IPlanService>
    {
        public PlanController(IPlanService service) : base(service)
        {
        }

        [HttpPost]
        public IActionResult Create([FromBody] PlanDTO plan)
        {
            if (_service.Create(plan, out List<ErrorMessage> errors)) return CreatedAtAction(nameof(Create), plan);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanDTOUpdate plan)
        {
            _service.Update(plan, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }
    }
}
