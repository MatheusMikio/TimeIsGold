using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs.Login;

namespace TimeIsGold.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Dados de login são obrigatórios.");
            }

            var logged = _loginService.Login(loginDto, out var messages);

            if (logged == null)
                return Unauthorized(new { Errors = messages.Select(m => m.Message) });

            return Ok(new
            {
                logged.Id,
                logged.Name,
                logged.Email,
                logged.Role
            });
        }
    }
}
