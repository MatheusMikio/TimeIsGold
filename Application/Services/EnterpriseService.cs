using Application.DTOs.Enterprise;
using Application.Services.Base;
using AutoMapper;
using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Base;
using Domain.Ports.Enterprise;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Application.Services
{
    public class EnterpriseService : BaseService<EnterpriseDTO, Enterprise, IEnterpriseRepository>, IEnterpriseService
    {
        public EnterpriseService(IEnterpriseRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public bool Create(EnterpriseDTO dto, out List<ErrorMessage> messages)
        {
            bool valid = Validate(dto, out messages, _repository);
            
            if (valid)
            {
                try
                {
                    Enterprise entity = _mapper.Map<Enterprise>(dto);
                    _repository.Create(entity);
                    return true;
                }
                catch
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar a empresa"));
                    return false;
                }
            }
            return false;
        }

        public void Update(EnterpriseDTOUpdate entity, out List<ErrorMessage> messages)
        {
            if (!ValidateUpdate(entity, out messages, _repository))
                return;

            Enterprise? enterpriseEntity = _repository.GetById<Enterprise>(entity.Id);

            if (enterpriseEntity == null)
            {
                messages.Add(new ErrorMessage("Enterprise", "Empresa não encontrada"));
                return;
            }

            try
            {
                enterpriseEntity.Name = entity.Name;
                enterpriseEntity.Cnpj = entity.Cnpj;
                enterpriseEntity.PlanId = entity.PlanId;
                enterpriseEntity.Address = entity.Address;
                _repository.Update(enterpriseEntity);
                return;
            }
            catch (Exception ex)
            {
                messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o cliente"));
                return;
            }
        }

        public static bool Validate(EnterpriseDTO enterprise, out List<ErrorMessage> messages, IEnterpriseRepository repository)
        {
            ValidationContext validationContext = new(enterprise);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(enterprise, validationContext, errors, true);
            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            if (string.IsNullOrEmpty(enterprise.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome da empresa é obrigatório."));
                validation = false;
            }

            if (string.IsNullOrEmpty(enterprise.Cnpj))
            {
                messages.Add(new ErrorMessage("Cnpj", "O CNPJ da empresa é obrigatório."));
                validation = false;
            }

            if (!ValidateCnpj(enterprise.Cnpj))
            {
                messages.Add(new ErrorMessage("Cnpj", "CNPJ inválido."));
                validation = false;
            }

            var existing = repository.GetByCnpj(enterprise.Cnpj);
            if (existing != null)
            {
                messages.Add(new ErrorMessage("Cnpj", "O CNPJ informado já está em uso por outra empresa."));
                validation = false;
            }

            if (enterprise.PlanId <= 0)
            {
                messages.Add(new ErrorMessage("Plano", "Plano inválido."));
                validation = false;
            }
            return validation;
        }

        public static bool ValidateUpdate(EnterpriseDTOUpdate enterprise, out List<ErrorMessage> messages, IBaseRepository repository)
        {
            ValidationContext validationContext = new(enterprise);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(enterprise, validationContext, errors, true);
            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            if (enterprise.Id <= 0)
            {
                messages.Add(new ErrorMessage("Id", "Id inválido."));
                validation = false;
            }

            if (string.IsNullOrWhiteSpace(enterprise.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome da empresa é obrigatório."));
                validation = false;
            }

            if (string.IsNullOrWhiteSpace(enterprise.Cnpj))
            {
                messages.Add(new ErrorMessage("Cnpj", "O CNPJ da empresa é obrigatório."));
                validation = false;
            }

            if (!ValidateCnpj(enterprise.Cnpj))
            {
                messages.Add(new ErrorMessage("Cnpj", "CNPJ inválido."));
                validation = false;
            }

            var existing = ((IEnterpriseRepository)repository).GetByCnpj(enterprise.Cnpj);
            if (existing != null && existing.Id != enterprise.Id)
            {
                messages.Add(new ErrorMessage("Cnpj", "O CNPJ informado já está em uso por outra empresa."));
                validation = false;
            }

            if (enterprise.PlanId <= 0)
            {
                messages.Add(new ErrorMessage("Plano", "Plano inválido."));
                validation = false;
            }

            return validation;
        }

        public static bool ValidateCnpj(string cnpj)
        {
            ReadOnlySpan<char> cnpjSpan = cnpj;
            Span<int> digits = stackalloc int[14];
            int count = 0;

            foreach (char cnpjNum in cnpjSpan)
            {
                if (char.IsDigit(cnpjNum))
                {
                    if (count >= 14) return false;
                    digits[count++] = cnpjNum - '0';
                }
            }

            if (count != 14) return false;

            bool allSame = true;
            for (int i = 1; i < 14; i++)
            {
                if (digits[i] != digits[0])
                {
                    allSame = false;
                    break;
                }
            }

            if (allSame) return false;

            static int CalculateCheckDigit(ReadOnlySpan<int> digits, int startWeight)
            {
                int sum = 0;
                int weight = startWeight;

                for (int i = 0; i < digits.Length; i++)
                {
                    sum += digits[i] * weight;
                    weight--;

                    if (weight < 2)
                    {
                        weight = 9;
                    }
                }

                int rest = sum % 11;
                return (rest < 2) ? 0 : 11 - rest;
            }

            int digitCheck1 = CalculateCheckDigit(digits.Slice(0, 12), 5);
            if (digitCheck1 != digits[12]) return false;

            int digitCheck2 = CalculateCheckDigit(digits.Slice(0, 13), 6);
            return digitCheck2 == digits[13];
        }
    }
}
