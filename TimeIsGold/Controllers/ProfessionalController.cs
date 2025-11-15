using Application.DTOs.Professional;
using Application.Services;
using Domain.DTOs.Login;
using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Professional;
using Microsoft.AspNetCore.Mvc;
using TimeIsGold.Controllers.Base;

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

            if (_service.Create(professionalDTO, out List<ErrorMessage> errors)) return CreatedAtAction(nameof(Create), professionalDTO);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ProfessionalDTOUpdate professional)
        {
            _service.Update(professional, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Dados de login são obrigatórios.");
            }

            ProfessionalDTOOutput logged = _service.Login(loginDto.Email, loginDto.Password, out List<ErrorMessage> messages);

            if (logged == null) return Unauthorized(messages);

            return Ok(logged);
        }
    }
}
