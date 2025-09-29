using Application.DTOs.Plan;
using Domain.Ports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : BaseController<PlanDTO, IPlanService>
    {
        public PlanController(IPlanService service) : base(service)
        {
        }
    }
}
