using Application.DTOs.Client;
using Domain.DTOs.Client;
using Domain.DTOs.Login;
using Domain.Entities;
using Domain.Ports.Client;
using Microsoft.AspNetCore.Mvc;

namespace TimeIsGold.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : BaseController<ClientDTOOutput, IClientService>
    {
        public ClientController(IClientService service) : base(service)
        {
        }

        [HttpPost]
        public IActionResult Create([FromBody] ClientDTO clientDTO)
        {
            if (_service.Create(clientDTO, out List<ErrorMessage> errors)) return CreatedAtAction(nameof(Create),clientDTO);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientDTOUpdate entity)
        {
            _service.Update(entity, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null) return BadRequest("Dados de login são obrigatórios.");
            
            ClientDTOOutput logged = _service.Login(loginDto.Email, loginDto.Password, out List<ErrorMessage> messages);

            if (logged == null) return Unauthorized(messages);

            return Ok(logged);
        }
    }
}