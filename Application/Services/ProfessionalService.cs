using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Application.DTOs.Professional;
using AutoMapper;
using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Professional;
using Domain.ValueObjects;

namespace Application.Services
{
    public class ProfessionalService : BaseService<ProfessionalDTO, Professional, IProfessionalRepository>, IProfessionalService
    {
        public ProfessionalService(IProfessionalRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public bool Create(ProfessionalDTO professionalDTO, out List<ErrorMessage> messages)
        {
            bool valid = Validate(professionalDTO, out messages, _repository);

            if (!valid)
                return false;

            try
            {
                Professional entity = _mapper.Map<Professional>(professionalDTO);
                entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(professionalDTO.Password); // HASH
                professionalDTO.Password = null;
                _repository.Create(entity);
                return true;
            }
            catch (Exception ex)
            {

                var errorMessage = ex.Message;

                //Pega o erro interno do EF
                if (ex.InnerException != null)
                    errorMessage += $" | Detalhe: {ex.InnerException.Message}";

                messages.Add(new ErrorMessage("Sistema", $"Erro inesperado ao salvar o profissional: {errorMessage}"));
                return false;
            }
        }

        public void Update(ProfessionalDTOUpdate entity, out List<ErrorMessage> messages)
        {
            bool valid = ValidateUpdate(entity, out messages, _repository);

            if (valid)
            {
                Professional? professionalEntity = _repository.GetById<Professional>(entity.Id);
                if (professionalEntity == null)
                {
                    messages.Add(new ErrorMessage("Professional", "Profissional não encontrado"));
                    return;
                }

                try
                {
                    // Atualiza os campos
                    professionalEntity.Name = entity.Name;
                    professionalEntity.Email = entity.Email;
                    professionalEntity.EnterpriseId = entity.EnterpriseId;
                    professionalEntity.Type = (ProfessionalType)entity.Type;
                    professionalEntity.Function = entity.Function;
                    professionalEntity.About = entity.About;
                    professionalEntity.ActuationTime = entity.ActuationTime;

                    if (!string.IsNullOrWhiteSpace(entity.Password))
                    {
                        professionalEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(entity.Password);
                        entity.Password = null;
                    }
                    _repository.Update(professionalEntity);
                }
                catch
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro ao atualizar profissional"));
                }
            }
        }
        public bool VerifyPassword(Professional professionalEntity, string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, professionalEntity.PasswordHash);
        }
        public static bool Validate(ProfessionalDTO professional, out List<ErrorMessage> messages, IProfessionalRepository repository)
        {
            ValidationContext context = new(professional);
            List<ValidationResult> results = new();
            bool validation = Validator.TryValidateObject(professional, context, results, true);

            messages = results.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            //Chamando validações
            if (!ValidateName(professional.Name, messages)) validation = false;
            if (!ValidatePhone(professional.Phone, messages)) validation = false;
            if (!ValidatePassword(professional.Password, messages)) validation = false;
            if (!ValidateFunction(professional.Function, messages)) validation = false;
            if (!ValidateAbout(professional.About, messages)) validation = false;
            if (!ValidateActuationTime(professional.ActuationTime, messages)) validation = false;
            if (!ValidateCRO(professional.CRO, messages)) validation = false;

            //cpf duplicado
            if (repository.CpfExists(professional.Cpf))
            {
                messages.Add(new ErrorMessage("Cpf", "Cpf já cadastrado."));
                validation = false;
            }

            //email duplicado
            if (repository.EmailExists(professional.Email))
            {
                messages.Add(new ErrorMessage("Email", "Email já cadastrado."));
                validation = false;
            }

            //empresa válida
            if (professional.EnterpriseId <= 0)
            {
                messages.Add(new ErrorMessage("EnterpriseId", "Empresa inválida."));
                validation = false;
            }

            //Tipo de profissional válido
            if (!Enum.IsDefined(typeof(ProfessionalType), professional.Type))
            {
                messages.Add(new ErrorMessage("Type", "Tipo de profissional inválido."));
                validation = false;
            }

            return validation;
        }

        public static bool ValidateUpdate(ProfessionalDTOUpdate professional, out List<ErrorMessage> messages, IProfessionalRepository repository)
        {
            ValidationContext context = new(professional);
            List<ValidationResult> results = new();
            bool validation = Validator.TryValidateObject(professional, context, results, true);

            messages = results.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            Professional? professionalDb = repository.GetById<Professional>(professional.Id);

            if (professionalDb == null)
            {
                messages.Add(new ErrorMessage("Professional", "Profissional não encontrado."));
                return false;
            }

            if (!ValidateFunction(professional.Function, messages)) validation = false;
            if (!ValidateAbout(professional.About, messages)) validation = false;
            if (!ValidateActuationTime(professional.ActuationTime, messages)) validation = false;

            //evita duplicações ao atualizar
            if (!ValidateName(professional.Name, messages)) validation = false;
            if (!ValidatePhone(professional.Phone, messages)) validation = false;

            // Checa duplicidade ignorando o próprio Id
            if (!repository.IsUnique(professional))
            {
                messages.Add(new ErrorMessage("Professional", "Já existe outro profissional com este e-mail ou CPF."));
                validation = false;
            }

            // Senha só se o usuário quiser trocar
            if (!string.IsNullOrWhiteSpace(professional.Password))
            {
                if (!ValidatePassword(professional.Password, messages)) validation = false;
            }

            if (!Enum.IsDefined(typeof(ProfessionalType), professional.Type))
            {
                messages.Add(new ErrorMessage("Type", "Tipo de profissional inválido."));
                validation = false;
            }
            return validation;
        }

        private static bool ValidateName(string name, List<ErrorMessage> messages)
        {
            if (string.IsNullOrEmpty(name))
            {
                messages.Add(new ErrorMessage("Name", "O nome é obrigatório."));
                return false;
            }
            if (name.Length < 3 || name.Length > 100)
            {
                messages.Add(new ErrorMessage("Name", "O nome do profissional deve ter entre 3 e 100 caracteres."));
                return false;
            }
            if (!Regex.IsMatch(name, @"^[a-zA-ZÀ-ÿ\s]+$"))
            {
                messages.Add(new ErrorMessage("Name", "O nome deve conter apenas letras e espaços."));
                return false;
            }
            return true;
        }

        private static bool ValidatePhone(string phone, List<ErrorMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                messages.Add(new ErrorMessage("Phone", "O telefone é obrigatório."));
                return false;
            }
            if (!Regex.IsMatch(phone, @"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$"))
            {
                messages.Add(new ErrorMessage("Phone", "O telefone fornecido é inválido. Use o formato (XX) XXXXX-XXXX."));
                return false;
            }
            return true;
        }

        private static bool ValidatePassword(string password, List<ErrorMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                messages.Add(new ErrorMessage("Password", "A senha é obrigatória."));
                return false;
            }
            if (password.Length < 8)
            {
                messages.Add(new ErrorMessage("Password", "A senha deve ter pelo menos 8 caracteres."));
                return false;
            }
            if (!Regex.IsMatch(password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).*$"))
            {
                messages.Add(new ErrorMessage("Password", "A senha deve conter pelo menos uma letra maiúscula, minúscula e número."));
                return false;
            }
            return true;
        }

        private static bool ValidateFunction(string? function, List<ErrorMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(function))
            {
                messages.Add(new ErrorMessage("Function", "A função do profissional é obrigatória."));
                return false;
            }

            if (function.Length < 2 || function.Length > 50)
            {
                messages.Add(new ErrorMessage("Function", "A função deve ter entre 2 e 50 caracteres."));
                return false;
            }
            return true;
        }

        private static bool ValidateAbout(string? about, List<ErrorMessage> messages)
        {
            if (!string.IsNullOrWhiteSpace(about) && about.Length > 500)
            {
                messages.Add(new ErrorMessage("About", "O campo 'Sobre' deve ter no máximo 500 caracteres."));
                return false;
            }
            return true;
        }

        private static bool ValidateActuationTime(string actuationTime, List<ErrorMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(actuationTime))
            {
                messages.Add(new ErrorMessage("ActuationTime", "O tempo de atuação é obrigatório."));
                return false;
            }

            if (actuationTime.Length > 50)
            {
                messages.Add(new ErrorMessage("ActuationTime", "O tempo de atuação deve ter no máximo 50 caracteres."));
                return false;
            }
            return true;
        }

        private static bool ValidateCRO(string cro, List<ErrorMessage> messages)
        {
            if (string.IsNullOrWhiteSpace(cro))
            {
                messages.Add(new ErrorMessage("CRO", "O CRO é obrigatório."));
                return false;
            }
            if (!Regex.IsMatch(cro, @"^CRO[-\s]?[A-Z]{2}\s?\d{1,6}$"))
            {
                messages.Add(new ErrorMessage("CRO", "Formato de CRO inválido. Ex: CRO-SP 12345."));
                return false;
            }
            return true;
        }
    }
}
