using Application.DTOs.Professional;
using Application.Services;
using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Professional;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProfessionalController : BaseController<ProfessionalDTOOutput, IProfessionalService>
    {
        private readonly IProfessionalService _service;
        public ProfessionalController(IProfessionalService service) : base(service)
        {
            _service = service;
        }

        //somente Admin
        [HttpPost("create")]
        public IActionResult Create([FromHeader] long requesterId, [FromBody] ProfessionalDTO professionalDto)
        {
            var requester = _service.GetById<Professional>(requesterId);
            if (requester == null || requester.Type != Domain.ValueObjects.ProfessionalType.Admin)
                return BadRequest(new { success = false, errors = new[] { "Apenas administradores podem criar profissionais." } });

            bool created = _service.Create(professionalDto, out List<ErrorMessage> messages);

            if (!created)
                return BadRequest(new { success = false, errors = messages });

            return Ok(new { success = true, message = "Profissional criado com sucesso!" });
        }

        //Admin pode todos e médico pode só o próprio
        [HttpPut("update")]
        public IActionResult Update([FromHeader] long requesterId, [FromBody] ProfessionalDTOUpdate professionalDTO)
        {
            var requester = _service.GetById<Professional>(requesterId);
            if (requester == null)
                return Unauthorized();

            if (requester.Type != ProfessionalType.Admin && requester.Id != professionalDTO.Id)
                return BadRequest(new { success = false, errors = new[] { "Você só pode editar o seu próprio perfil." } });

            _service.Update(professionalDTO, out List<ErrorMessage> errors);

            if (errors.Any())
                return BadRequest(new { success = false, errors });

            return Ok(new { success = true, message = "Profissional atualizado com sucesso!" });
        }

        //somente Admin
        [HttpGet("all")]
        public IActionResult GetAll([FromHeader] long requesterId, [FromQuery] int page = 1, [FromQuery] int size = 20, [FromQuery] string text = null)
        {
            var requester = _service.GetById<Professional>(requesterId);
            if (requester == null || requester.Type != ProfessionalType.Admin)
                return Forbid("Apenas administradores podem visualizar todos os profissionais.");

            return Ok(_service.GetAll<ProfessionalDTO>(page, size, text));
        }

        //somente Admin
        [HttpDelete("{id}")]
        public IActionResult Delete(long id, [FromHeader] long requesterId)
        {
            var requester = _service.GetById<Professional>(requesterId);
            if (requester == null || requester.Type != ProfessionalType.Admin)
                return Forbid("Apenas administradores podem excluir profissionais.");

            bool deleted = _service.Delete<Professional>(id);
            if (!deleted) return NotFound("Profissional não encontrado");

            return Ok(new { success = true, message = "Apagado com sucesso!" });
        }
    }
}
