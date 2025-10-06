using Application.DTOs.Plan;
using AutoMapper;
using Domain.DTOs.Plan;
using Domain.Entities;
using Domain.Ports.Plan;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PlanService : BaseService<PlanDTO, Plan, IPlanRepository>, IPlanService
    {
        public PlanService(IPlanRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public bool Create(PlanDTO planDTO, out List<ErrorMessage> messages)
        {
            bool valid = Validate(planDTO, out messages, _repository);

            if (valid)
            {
                try
                {
                    Plan planEntity = _mapper.Map<Plan>(planDTO);
                    _repository.Create(planEntity);
                    return true;
                }
                catch (Exception ex)
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o plano"));
                    return false;
                }
            }
            return false;
        }

        public void Update(PlanDTOUpdate entity, out List<ErrorMessage> messages)
        {
            bool valid = ValidateUpdate(entity, out messages, _repository);

            if (valid)
            {
                Plan ? planEntity = _repository.GetById<Plan>(entity.Id);

                if (planEntity == null)
                {
                    messages.Add(new ErrorMessage("Plano", "Plano não encontrado"));
                    return;
                }

                try
                {
                    planEntity.Value = entity.Value;
                    planEntity.Level = (PlanLevel)entity.Level;
                    planEntity.ChangedAt = DateTime.UtcNow;
                    planEntity.ScheduleTypeLimit = entity.ScheduleTypeLimit;
                    _repository.Update(planEntity);
                }
                catch (Exception ex)
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o plano"));
                }
            }
        }

        public static bool Validate(PlanDTO plan, out List<ErrorMessage> messages, IPlanRepository repository)
        {
            ValidationContext validationContext = new(plan);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(plan, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            if (!Enum.IsDefined(typeof(PlanLevel), (PlanLevel)plan.Level))
            {
                messages.Add(new ErrorMessage("Nível", "Nivel do plano indisponivel"));
                validation = false;
            }

            if (plan.Value < 100)
            {
                messages.Add(new ErrorMessage("Plano", "O valor do plano não pode ser menor que R$100,00"));
                validation = false;
            }

            if (plan.ScheduleTypeLimit < 5)
            {
                messages.Add(new ErrorMessage("Plano", "O numero limite de agendamentos não pode ser inferior a 5."));
                validation = false;
            }

            return validation;
        }
    

        public static bool ValidateUpdate(PlanDTOUpdate plan, out List<ErrorMessage> messages, IPlanRepository repository)
        {
            ValidationContext validationContext = new(plan);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(plan, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            if (!repository.IsUnique(plan))
            {
                messages.Add(new ErrorMessage("Plano", "Já existe um plano nesse formato"));
                validation = false;
                return validation;
            }

            Plan ? planDb = repository.GetById<Plan>(plan.Id);

            if (planDb == null)
            {
                messages.Add(new ErrorMessage("Plano", "Plano não encontrado")) ;
                validation = false;
                return validation;
            }

            if (!Enum.IsDefined(typeof(PlanLevel), (PlanLevel)plan.Level))
            {
                messages.Add(new ErrorMessage("Nível", "Nivel do plano indisponivel"));
                validation = false;
            }

            if (plan.Value < 100)
            {
                messages.Add(new ErrorMessage("Plano", "O valor do plano não pode ser menor que R$100,00"));
                validation = false;
            }

            if (plan.ScheduleTypeLimit < 5)
            {
                messages.Add(new ErrorMessage("Plano", "O numero limite de agendamentos não pode ser inferior a 5."));
                validation = false;
            }

            return validation;
        }
    } 
}

