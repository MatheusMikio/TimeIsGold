using Application.DTOs.Plan;
using Domain.DTOs.Plan;
using Domain.Entities;
using Domain.Ports.Plan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeIsGold.Controllers.Base;

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
        public IActionResult Create([FromBody] PlanDTO planDTO)
        {
            if (_service.Create(planDTO, out List<ErrorMessage> errors)) return CreatedAtAction(nameof(Create), planDTO);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanDTOUpdate entity)
        {
            _service.Update(entity, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }
    }
}