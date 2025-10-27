using Application.DTOs.Enterprise;
using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Enterprise;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterpriseController : BaseController<EnterpriseDTOOutput, IEnterpriseService>
    {

        public EnterpriseController(IEnterpriseService service) : base(service)
        {
        }

        [HttpPost]
        public IActionResult Create([FromBody] EnterpriseDTO enterpriseDTO)
        {
            var enterprise = _service.Create(enterpriseDTO, out List<ErrorMessage> errors);
            if (enterprise != null)
                return Ok(enterprise);

            return NotFound();
        }

        [HttpPut]
        public IActionResult Update([FromBody] EnterpriseDTOUpdate entity)
        {
            _service.Update(entity, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var enterprise = _service.GetById(id);

            if (enterprise != null) return Ok(enterprise);

            return BadRequest();
        }
    }
}