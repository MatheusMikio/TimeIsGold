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

        public static bool Validate(EnterpriseDTO enterprise, out List<ErrorMessage> messages, IBaseRepository repository)
        {
            messages = new List<ErrorMessage>();
            bool isValid = true;

            if (string.IsNullOrEmpty(enterprise.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome da empresa é obrigatório."));
                isValid = false;
            }

            if (string.IsNullOrEmpty(enterprise.Cnpj))
            {
                messages.Add(new ErrorMessage("CNPJ", "O CNPJ da empresa é obrigatório."));
                isValid = false;
            }
            else
            {
                var existing = repository.GetByTextFilter<Enterprise>(1, 1, enterprise.Cnpj).FirstOrDefault();
                if (existing != null)
                {
                    messages.Add(new ErrorMessage("CNPJ", "O CNPJ informado já está em uso por outra empresa."));
                    isValid = false;
                }
            }

            if (enterprise.PlanId <= 0)
            {
                messages.Add(new ErrorMessage("Plano", "Plano inválido."));
                isValid = false;
            }

           return isValid;
        }

        public static bool ValidateUpdate(EnterpriseDTOUpdate enterprise, out List<ErrorMessage> messages, IBaseRepository repository)
        {
            messages = new List<ErrorMessage>();
            bool isValid = true;

            if (enterprise.Id <= 0)
            {
                messages.Add(new ErrorMessage("Id", "Id inválido."));
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(enterprise.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome da empresa é obrigatório."));
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(enterprise.Cnpj))
            {
                messages.Add(new ErrorMessage("Cnpj", "O CNPJ da empresa é obrigatório."));
                isValid = false;
            }
            else
            {
                var existing = repository.GetByTextFilter<Enterprise>(1, 1, enterprise.Cnpj).FirstOrDefault(e => e.Id != enterprise.Id);

                if (existing != null)
                { 
                    messages.Add(new ErrorMessage("Cnpj", "O CNPJ informado já está em uso por outra empresa."));
                    isValid = false;
                }
            }

            if (enterprise.PlanId <= 0)
            {
                messages.Add(new ErrorMessage("Plano", "Plano inválido."));
                isValid = false;
            }

            return isValid;
        }
    }
}
