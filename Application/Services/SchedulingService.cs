using Application.DTOs.Scheduling;
using AutoMapper;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Scheduling;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    //private readonly IClientRepository _clientRepository;
    //private readonly IProfessionalRepository _professionalRepository;
    //private readonly IEnterpriseRepository _enterpriseRepository;
    public class SchedulingService : BaseService<SchedulingDTO, Scheduling, ISchedulingRepository>, ISchedulingService
    {
        public SchedulingService(
            ISchedulingRepository repository,
            IMapper mapper/*,
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository
            IEnterpriseRepository enterpriseRepository*/
        ) : base(
            repository,
            mapper
            )
        {
            /*_clientRepository = clientRepository
             _professionalRepository = professionalRepository
            _enterpriseRepository = enterpriseRepository;*/
        }

        public int GetTodaySchedulings(long id, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();

            Enterprise? enterprise = _repository.GetById<Enterprise>(id);

            if (enterprise == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada"));
                return 0;
            }

            return _repository.GetTodaySchedulings(id);
        }

        public int GetPendentsSchedulings(long id, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();
            Enterprise? enterprise = _repository.GetById<Enterprise>(id);
            if (enterprise == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada"));
                return 0;
            }
            return _repository.GetPendentsSchedulings(id);
        }

        public List<SchedulingDTOOutput> GetSchedulingsByPeriod(long id, PeriodType periodType, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();
            Enterprise? enterprise = _repository.GetById<Enterprise>(id);
            if (enterprise == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada"));
                return new List<SchedulingDTOOutput>();
            }

            if (!Enum.IsDefined(typeof(PeriodType), periodType))
            {
                messages.Add(new ErrorMessage("Período", "Periodo inválido"));
                return new List<SchedulingDTOOutput>();
            }

            List<Scheduling> schedulings = _repository.GetSchedulingsByPeriod(id, periodType);
            return _mapper.Map<List<SchedulingDTOOutput>>(schedulings);
        }

        public bool Create(SchedulingDTO schedulingDTO, out List<ErrorMessage> messages)
        {
            bool valid = Validate(schedulingDTO,
                out messages, 
                _repository/*,
                *IClientRepository clientRepository,
             *  IProfessionalRepository professionalRepository,
             *  IEnterpriseRepository enterpriseRepository*/
            );

            if (valid)
            {
                try
                {
                    Scheduling scheduling = _mapper.Map<Scheduling>(schedulingDTO);
                    _repository.Create(scheduling);
                    return true;
                }
                catch (Exception ex) 
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao salvar o agendamento"));
                    return false;
                }
            }
            return false;
        }

        public void Update(SchedulingDTOUpdate schedulingDTO, out List<ErrorMessage> messages)
        {
            bool valid = ValidateUpdate(schedulingDTO,
                out messages,
                _repository/*,
                *IClientRepository clientRepository,
                *IProfessionalRepository professionalRepository,
                *IEnterpriseRepository enterpriseRepository*/);

            if (valid)
            {
                Scheduling schedulingDb = _repository.GetById<Scheduling>(schedulingDTO.Id);
                try
                {
                    schedulingDb.ClientId = schedulingDTO.ClientId;
                    schedulingDb.ProfessionalId = schedulingDTO.ProfessionalId;
                    schedulingDb.EnterpriseId = schedulingDTO.EnterpriseId;
                    schedulingDb.ScheduledDate = schedulingDTO.ScheduledDate;
                    schedulingDb.Status = (Status)schedulingDTO.Status;
                    schedulingDb.ChangedAt = DateTime.UtcNow;
                    _repository.Update(schedulingDb);
                }
                catch
                {
                    messages.Add(new ErrorMessage("Sistema", "Erro inesperado ao atualizar o agendamento"));
                    return;
                }
            }
        }

        public static bool Validate(
            SchedulingDTO scheduling,
            out List<ErrorMessage> messages,
            ISchedulingRepository repository/*,
            *IClientRepository clientRepository,
            *IProfessionalRepository professionalRepository,
            *IEnterpriseRepository enterpriseRepository*/
        )
        {
            ValidationContext validationContext = new(scheduling);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(scheduling, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            //Professional ? professionalDb = clientRepository.GetById<Professional>(scheduling.ProfessionalId);
            //if (professionalDb == null)
            //{
            //    messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
            //    return false;
            //}

            //Client ? clientDb = clientRepository.GetById<Client>(scheduling.ClientId);
            //if (clientDb == null)
            //{
            //    messages.Add(new ErrorMessage("Cliente", "Cliente não encontrado"));
            //    return false;
            //}

            //Enterprise ? enterpriseDb = enterpriseRepository.GetById<Enterprise>(scheduling.EnterpriseId);
            //if (enterpriseDb == null)
            //{
            //    messages.Add(new ErrorMessage("Cliente", "Cliente não encontrado"));
            //    return false;
            //}

            if (scheduling.ScheduledDate < DateTime.UtcNow)
            {
                messages.Add(new ErrorMessage("Data", "A data agendada não pode ser no passado"));
                return false;
            }

            bool schedulingDb = repository.GetSchedulingByDate(scheduling.ProfessionalId, scheduling.ClientId, scheduling.ScheduledDate);

            if (schedulingDb)
            {
                messages.Add(new ErrorMessage("Data", "Não é possivel criar um agendamento nesse horário."));
                return false;
            }
            return validation;
        }

        public static bool ValidateUpdate(
            SchedulingDTOUpdate scheduling,
            out List<ErrorMessage> messages,
            ISchedulingRepository repository/*,
            *IClientRepository clientRepository,
            *IProfessionalRepository professionalRepository,
            *IEnterpriseRepository enterpriseRepository*/

        )
        {
            ValidationContext validationContext = new(scheduling);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(scheduling, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            Scheduling ? schedulingDb = repository.GetById<Scheduling>(scheduling.Id);
            if (schedulingDb == null)
            {
                messages.Add(new ErrorMessage("Agendamento", "Agendamento não encontrado"));
                return false;
            }

            //Professional ? professionalDb = clientRepository.GetById<Professional>(scheduling.ProfessionalId);
            //if (professionalDb == null)
            //{
            //    messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
            //    return false;
            //}

            //Client ? clientDb = clientRepository.GetById<Client>(scheduling.ClientId);
            //if (clientDb == null)
            //{
            //    messages.Add(new ErrorMessage("Cliente", "Cliente não encontrado"));
            //    return false;
            //}

            //Enterprise ? enterpriseDb = enterpriseRepository.GetById<Enterprise>(scheduling.EnterpriseId);
            //if (enterpriseDb == null)
            //{
            //    messages.Add(new ErrorMessage("Cliente", "Cliente não encontrado"));
            //    return false;
            //}

            if (scheduling.ScheduledDate < DateTime.UtcNow)
            {
                messages.Add(new ErrorMessage("Data", "A data agendada não pode ser no passado"));
                return false;
            }

            if (!repository.IsUnique(scheduling))
            {
                messages.Add(new ErrorMessage("Data", "Já existe um agendamento para este profissional ou cliente nesta data e hora"));
                return false;
            }

            return validation;
        }
    }
}
