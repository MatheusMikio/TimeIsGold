using Application.DTOs.Plan;
using AutoMapper;
using Domain.DTOs.Plan;
using Domain.Entities;
using Domain.Ports;
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
                try
                {
                    Plan plaEntity = _mapper.Map<Plan>(entity);
                    _repository.Update(plaEntity);
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

            if (plan.Level < 1 || plan.Level > 3)
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
            }

            return validation;
        }
    

        public static bool ValidateUpdate(PlanDTOUpdate plan, out List<ErrorMessage> messages, IPlanRepository repository)
        {
            ValidationContext validationContext = new(plan);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(plan, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            Plan? planDb = repository.GetById<Plan>(plan.Id);

            if (planDb == null)
            {
                messages.Add(new ErrorMessage("Plano", "Plano não encontrado")) ;
                validation = false;
            }

            if (plan.Level < 1 || plan.Level > 3)
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
            }

            return validation;
        }
    } 
}

