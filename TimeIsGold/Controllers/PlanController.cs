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
            return base.Create(plan);
        }

        [HttpPut]
        public IActionResult Update([FromBody] PlanDTOUpdate plan)
        {
            return base.Update(plan);
        }
    }
}
