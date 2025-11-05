using Application.DTOs.Professional;
using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Professional;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProfessionalController : BaseController<ProfessionalDTOOutput, IProfessionalService>
    {
        public ProfessionalController(IProfessionalService service) : base(service)
        {
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProfessionalDTO professionalDTO)
        {
            var professional = _service.Create(professionalDTO, out List<ErrorMessage> errors);
            if (professional != false)
                return Ok("Profissional criado com sucesso!");

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProfessionalDTOUpdate professional)
        {
            _service.Update(professional, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            if (errors.Any(e => e.Property == "Professional" && e.Message.Contains("não encontrado")))
                return NotFound(errors);

            return BadRequest(errors);
        }
    }
}
