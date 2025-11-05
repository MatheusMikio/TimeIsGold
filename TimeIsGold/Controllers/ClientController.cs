using Application.DTOs.Client;
using Domain.DTOs.Client;
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
            if (clientDTO == null)
                return BadRequest("O corpo da requisição não pode ser vazio.");

            if (_service.Create(clientDTO, out List<ErrorMessage> errors))
                return Ok(clientDTO);

            return UnprocessableEntity(errors);
        }

        [HttpPut]
        public IActionResult Update([FromBody] ClientDTOUpdate entity)
        {
            _service.Update(entity, out List<ErrorMessage> errors);

            if (errors.Count == 0) return NoContent();

            return BadRequest(errors);
        }
    }
}