using Application.DTOs.Scheduling;
using Application.DTOs.SchedulingType;
using AutoMapper;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
using Domain.Ports.SchedulingType;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SchedulingTypeService : BaseService<SchedulingDTO, Scheduling, ISchedulingTypeRepository>, ISchedulingTypeService
    {
        //private readonly IEnterpriseRepository _enterpriseRepository;
        public SchedulingTypeService(
            ISchedulingTypeRepository SchedulingTyperepository,
            IMapper mapper/*,
            IEnterpriseRepository entepriseRepository*/
        ) : base(
            SchedulingTyperepository,
            mapper
            )
        {
            //_entepriseRepository = entepriseRepository; 
        }

        public bool Create(SchedulingTypeDTO schedulingType, out List<ErrorMessage> messages)
        {
            bool valid = Validate(schedulingType, out messages, _repository);
            
            if (valid)
            {
                try
                {
                    SchedulingType schedulingTypeEntity = _mapper.Map<SchedulingType>(schedulingType);
                    _repository.Create(schedulingTypeEntity);
                    return true;
                }
                catch (Exception ex)
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o tipo de agendamento"));
                    return false;
                }
            }
            return false;
        }

        public void Update(SchedulingTypeDTOUpdate entity, out List<ErrorMessage> messages)
        {
            bool valid = ValidateUpdate(entity, out messages, _repository/*, _enterpriseRepository*/);

            if (valid)
            {
                SchedulingType ? schedulingTypeDb = _repository.GetById<SchedulingType>(entity.Id);

                if ( schedulingTypeDb == null)
                {
                    messages.Add(new ErrorMessage("Tipo de Agendamento", "Tipo de agendamento não encontrado"));
                    return;
                }

                try
                {
                    schedulingTypeDb.Name = entity.Name;
                    schedulingTypeDb.Description = entity.Description;
                    schedulingTypeDb.Value = entity.Value;
                    schedulingTypeDb.ChangedAt = DateTime.UtcNow;
                    _repository.Update(schedulingTypeDb);
                }
                catch (Exception ex)
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o tipo de agendamento"));
                }
            }
        }


        public static bool Validate(
            SchedulingTypeDTO scheduling,
            out List<ErrorMessage> messages,
            ISchedulingTypeRepository SchedulingTyperepository/*,
            *IEnterpiseRepository enterpriseRepository*/
        )
        {
            ValidationContext validationContext = new(scheduling);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(scheduling, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            if (string.IsNullOrEmpty(scheduling.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome é obrigatório"));
                return false;
            }

            if (string.IsNullOrEmpty(scheduling.Description))
            {
                messages.Add(new ErrorMessage("Descrição", "A descrição é obrigatória"));
                return false;
            }

            if (scheduling.Value <= 0)
            {
                messages.Add(new ErrorMessage("Valor", "O valor do tipo de agendamento não pode ser negativo"));
                return false;
            }

            /*Enterprise ? entepriseDb = entepriseRepository.GetById(scheduling.EnterpriseId)
             if (enterpriseDb == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada.")
                return false;
            }*/

            if (scheduling.Name.Length < 3)
            {
                messages.Add(new ErrorMessage("Nome", "O nome deve ter no mínimo 3 caracteres"));
                validation = false;
            }

            if (SchedulingTyperepository.GetByName(scheduling.Name, scheduling.EnterpriseId))
            {
                messages.Add(new ErrorMessage("Nome", "Já existe um tipo de agendamento com esse nome"));
                validation = false;
            }

            if (scheduling.Description.Length < 3)
            {
                messages.Add(new ErrorMessage("Descrição", "A descrição deve ter no mínimo 3 caracteres"));
                validation = false;
            }

            return validation;
        }

        public static bool ValidateUpdate(
            SchedulingTypeDTOUpdate schedulingType,
            out List<ErrorMessage> messages,
            ISchedulingTypeRepository schedulingTypeRepository/*,
            *IEnterpiseRepository enterpriseRepository*/
        )
        {
            ValidationContext validationContext = new(schedulingType);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(schedulingType, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            if (!schedulingTypeRepository.IsUnique(schedulingType))
            {
                messages.Add(new ErrorMessage("Tipo de Agendamento", "Já existe um tipo de agendamento com esse nome"));
                return false;
            }


            if (string.IsNullOrEmpty(schedulingType.Name))
            {
                messages.Add(new ErrorMessage("Nome", "O nome é obrigatório"));
                return false;
            }

            if (string.IsNullOrEmpty(schedulingType.Description))
            {
                messages.Add(new ErrorMessage("Descrição", "A descrição é obrigatória"));
                return false;
            }

            if (schedulingType.Value <= 0)
            {
                messages.Add(new ErrorMessage("Valor", "O valor do tipo de agendamento não pode ser negativo"));
                return false;
            }

            /*Enterprise ? entepriseDb = entepriseRepository.GetById(scheduling.EnterpriseId)
             if (enterpriseDb == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada.")
                return false;
            }*/

            if (schedulingType.Name.Length < 3)
            {
                messages.Add(new ErrorMessage("Nome", "O nome deve ter no mínimo 3 caracteres"));
                validation = false;
            }

            if (schedulingType.Description.Length < 3)
            {
                messages.Add(new ErrorMessage("Descrição", "A descrição deve ter no mínimo 3 caracteres"));
                validation = false;
            }

            return validation;
        }
    }
}
