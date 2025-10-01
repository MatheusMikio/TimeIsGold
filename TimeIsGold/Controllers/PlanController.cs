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
        public IActionResult Create([FromBody] PlanDTO planDTO)
        {
            if (_service.Create(planDTO, out List<ErrorMessage> erros)) return CreatedAtAction(nameof(Create), planDTO);

            return UnprocessableEntity(erros);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanDTOUpdate entity)
        {
            _service.Update(entity, out List<ErrorMessage> erros);

            if (erros.Count == 0) return NoContent();

            return BadRequest(erros);
        }
    }
}
