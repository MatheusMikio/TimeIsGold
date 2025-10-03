using Application.DTOs.Scheduling;
using Application.DTOs.SchedulingType;
using AutoMapper;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
using Domain.Ports;
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
        public SchedulingTypeService(ISchedulingTypeRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public bool Create(SchedulingTypeDTO planDTO, out List<ErrorMessage> messages)
        {
            bool valid = Validate(planDTO, out messages, _repository);
        }

        public void Update(SchedulingTypeDTOUpdate entity, out List<ErrorMessage> messages)
        {
            throw new NotImplementedException();
        }


        public static bool Validate(SchedulingTypeDTO scheduling, out List<ErrorMessage> messages, ISchedulingTypeRepository repository)
        {
            ValidationContext validationContext = new(scheduling);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(scheduling, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            if (string.IsNullOrEmpty(scheduling.Description))
            {
                messages.Add(new ErrorMessage("Descrição", "A descrição é obrigatória"));
                return false;
            }

            if (repository.GetByName(scheduling) != null)
            {
                messages.Add(new ErrorMessage("Nome", "Já existe um tipo de agendamento com esse nome"));
                validation = false;
            }

            return validation;
        }
    }
}
