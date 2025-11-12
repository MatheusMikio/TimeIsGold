using AutoMapper;
using Domain.DTOs.Login;
using Domain.Entities;
using Domain.Ports.Client;
using Domain.Ports.Professional;
using Domain.ValueObjects;

namespace Application.Services
{
    public class LoginService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IMapper _mapper;

        public LoginService(
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository,
            IMapper mapper)
        {
            _clientRepository = clientRepository;
            _professionalRepository = professionalRepository;
            _mapper = mapper;
        }

        public LoggedDTO? Login(LoginDTO loginDto, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();

            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                messages.Add(new ErrorMessage("Login", "E-mail e senha são obrigatórios."));
                return null;
            }

            var admin = _professionalRepository.GetByEmailAndType(loginDto.Email, ProfessionalType.Admin);
            if (admin != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, admin.PasswordHash))
                return CreateLoggedDTO(admin, "Admin");

            var professional = _professionalRepository.GetByEmail(loginDto.Email);
            if (professional != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, professional.PasswordHash))
                return CreateLoggedDTO(professional, "Professional");

            var client = _clientRepository.GetByEmail(loginDto.Email);
            if (client != null && BCrypt.Net.BCrypt.Verify(loginDto.Password, client.PasswordHash))
                return CreateLoggedDTO(client, "Client");

            messages.Add(new ErrorMessage("Login", "E-mail ou senha incorretos."));
            return null;
        }

        private static LoggedDTO CreateLoggedDTO(dynamic user, string role)
        {
            return new LoggedDTO
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = role
            };
        }
    }
}
