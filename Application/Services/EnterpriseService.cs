using Application.DTOs.Enterprise;
using AutoMapper;
using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Base;
using Domain.Ports.Enterprise;

namespace Application.Services
{
    public class EnterpriseService : BaseService<EnterpriseDTO, Enterprise, IEnterpriseRepository>, IEnterpriseService
    {
        public EnterpriseService(IEnterpriseRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public bool Create(EnterpriseDTO dto, out List<ErrorMessage> messages)
        {
            if (!Validate(dto, out messages, _repository))
                return false;

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
                enterpriseEntity.Address = entity.address;
                _repository.Update(enterpriseEntity);
            }
            catch (Exception ex)
            {
                messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o cliente"));
            }
        }

        public static bool Validate(EnterpriseDTO dto, out List<ErrorMessage> messages, IBaseRepository repository)
        {
            messages = new List<ErrorMessage>();
            bool isValid = true;

            if (string.IsNullOrEmpty(dto.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome da empresa é obrigatório."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(dto.Cnpj))
            {
                messages.Add(new ErrorMessage("CNPJ", "O CNPJ da empresa é obrigatório."));
                isValid = false;
            }
            else
            {
                var existing = repository.GetByTextFilter<Enterprise>(1, 1, dto.Cnpj).FirstOrDefault();
                if (existing != null)
                {
                    messages.Add(new ErrorMessage("CNPJ", "O CNPJ informado já está em uso por outra empresa."));
                    isValid = false;
                }
            }

            if (dto.PlanId <= 0)
            {
                messages.Add(new ErrorMessage("Plano", "Plano inválido."));
                isValid = false;
            }

           return isValid;
        }

        public static bool ValidateUpdate(EnterpriseDTOUpdate dto, out List<ErrorMessage> messages, IBaseRepository repository)
        {
            messages = new List<ErrorMessage>();
            bool isValid = true;

            if (dto.Id <= 0)
            {
                messages.Add(new ErrorMessage("Id", "Id inválido."));
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome da empresa é obrigatório."));
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(dto.Cnpj))
            {
                messages.Add(new ErrorMessage("Cnpj", "O CNPJ da empresa é obrigatório."));
                isValid = false;
            }
            else
            {
                var existing = repository.GetByTextFilter<Enterprise>(1, 1, dto.Cnpj).FirstOrDefault(e => e.Id != dto.Id);

                if (existing != null)
                { 
                    messages.Add(new ErrorMessage("Cnpj", "O CNPJ informado já está em uso por outra empresa."));
                    isValid = false;
                }
            }

            if (dto.PlanId <= 0)
            {
                messages.Add(new ErrorMessage("Plano", "Plano inválido."));
                isValid = false;
            }

            return isValid;
        }
    }
}
