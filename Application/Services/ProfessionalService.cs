using System.ComponentModel.DataAnnotations;
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
        public Professional? GetById(long id)
        {
            return _repository.GetById<Professional>(id);
        }

        public bool Create(ProfessionalDTO professionalDTO, out List<ErrorMessage> messages)
        {
            bool valid = Validate(professionalDTO, out messages, _repository);

            if (!valid)
                return false;
            
            try
            {
                Professional entity = _mapper.Map<Professional>(professionalDTO);
                _repository.Create(entity);
                return true;
            }
            catch
            {
                messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o profissional"));
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
                    professionalEntity.Name = entity.Name;
                    professionalEntity.Cpf = entity.Cpf;
                    professionalEntity.Email = entity.Email;
                    professionalEntity.Password = entity.Password;
                    professionalEntity.EnterpriseId = entity.EnterpriseId;
                    professionalEntity.Type = (ProfessionalType)entity.Type;
                    professionalEntity.Function = entity.Function;
                    _repository.Update(professionalEntity);
                }
                catch
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro ao atualizar profissional"));
                }
            }
        }
        public static bool Validate(ProfessionalDTO professional, out List<ErrorMessage> messages, IProfessionalRepository repository)
        {
            ValidationContext context = new(professional);
            List<ValidationResult> results = new();
            bool validation = Validator.TryValidateObject(professional, context, results, true);

            messages = results.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

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

        public static bool ValidateUpdate(ProfessionalDTOUpdate professional, out List<ErrorMessage> messages,IProfessionalRepository repository)
        {
            ValidationContext context = new(professional);
            List<ValidationResult> results = new();
            bool validation = Validator.TryValidateObject(professional, context, results, true);

            messages = results.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            Professional? existing = repository.GetById<Professional>(professional.Id);
            if (existing == null)
            {
                messages.Add(new ErrorMessage("Professional", "Profissional não encontrado."));
                validation = false;

            }

            //evita duplicações ao atualizar
            if (repository.CpfExists(professional.Cpf, professional.Id))
            {
                messages.Add(new ErrorMessage("Cpf", "Cpf já cadastrado."));
                validation = false;
            }

            if (repository.EmailExists(professional.Email, professional.Id))
            {
                messages.Add(new ErrorMessage("Email", "Já existe outro profissional com este email"));
                validation = false;
            }

            if (!Enum.IsDefined(typeof(ProfessionalType), professional.Type))
            {
                messages.Add(new ErrorMessage("Type", "Tipo de profissional inválido."));
                validation = false;
            }
            return validation;
        }
    }
}
