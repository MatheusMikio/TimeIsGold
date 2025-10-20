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
        private readonly IEnterpriseService _service;

        public EnterpriseController(IEnterpriseService service) : base(service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] EnterpriseDTO enterpriseDTO)
        {
            if (_service.Create(enterpriseDTO, out List<ErrorMessage> errors))
                return CreatedAtAction(nameof(Create), enterpriseDTO);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] EnterpriseDTOUpdate entity)
        {
            _service.Update(entity, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }
    }
}
