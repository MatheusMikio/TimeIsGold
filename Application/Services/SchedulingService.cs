using Application.DTOs.Scheduling;
using Application.Services.Base;
using AutoMapper;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Client;
using Domain.Ports.Enterprise;
using Domain.Ports.Professional;
using Domain.Ports.Scheduling;
using Domain.Ports.SchedulingType;
using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class SchedulingService : BaseService<SchedulingDTO, Scheduling, ISchedulingRepository>, ISchedulingService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IProfessionalRepository _professionalRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private ISchedulingTypeRepository _schedulingTypeRepository;

        public SchedulingService(
            ISchedulingRepository repository,
            ISchedulingTypeRepository schedulingTypeRepository,
            IMapper mapper,
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository,
            IEnterpriseRepository enterpriseRepository
        ) : base(
            repository,
            mapper
            )
        {
            _clientRepository = clientRepository;
            _professionalRepository = professionalRepository;
            _enterpriseRepository = enterpriseRepository;
            _schedulingTypeRepository = schedulingTypeRepository;
        }

        public int GetTodaySchedulings(long id, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();

            Enterprise ? enterpriseDb = _repository.GetById<Enterprise>(id);

            if (enterpriseDb == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada"));
                return 0;
            }

            return _repository.GetTodaySchedulings(id);
        }

        public List<SchedulingDTOOutput> GetSchedulingToday(long id, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();
            Professional ? professional = _repository.GetById<Professional>(id);
            if (professional == null)
            {
                messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
                return new List<SchedulingDTOOutput>();
            }

            List<Scheduling> schedulings = _repository.GetSchedulingsProfessional(id);

            return _mapper.Map<List<SchedulingDTOOutput>>(schedulings);
        }

        public SchedulingStatisticsDTO GetTodaySchedulingsProfessional(long id, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();

            Professional ? professional = _repository.GetById<Professional>(id);

            if (professional == null)
            {
                messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
                return new SchedulingStatisticsDTO();
            }

            var statistics = _repository.GetTodaySchedulingsProfessional(id);

            return new SchedulingStatisticsDTO
            {
                Total = statistics.ContainsKey((Status)0) ? statistics[(Status)0] : statistics.Values.Sum(),
                Pendent = statistics[Status.Pendent],
                InProgress = statistics[Status.InProgress],
                Finished = statistics[Status.Finished],
                Canceled = statistics[Status.Canceled]
            };
        }

        public int GetPendentsSchedulings(long id, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();
            Enterprise ? enterpriseDb = _repository.GetById<Enterprise>(id);
            if (enterpriseDb == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada"));
                return 0;
            }
            return _repository.GetPendentsSchedulings(id);
        }

        public List<SchedulingDTOOutput> GetSchedulingsByPeriod(long id, PeriodType periodType, out List<ErrorMessage> messages)
        {
            messages = new List<ErrorMessage>();
            Professional ? professional = _repository.GetById<Professional>(id);
            if (professional == null)
            {
                messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
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
                _repository, _schedulingTypeRepository,
                _clientRepository,
                _professionalRepository,
                _enterpriseRepository
            );

            if (valid)
            {
                try
                {
                    Scheduling scheduling = _mapper.Map<Scheduling>(schedulingDTO);
                    scheduling.Status = Status.Pendent;
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
                _repository,
                _schedulingTypeRepository,
                _clientRepository,
                _professionalRepository,
                _enterpriseRepository);

            if (valid)
            {
                Scheduling schedulingDb = _repository.GetById<Scheduling>(schedulingDTO.Id);
                try
                {
                    schedulingDb.ProfessionalId = schedulingDTO.ProfessionalId;
                    schedulingDb.EnterpriseId = schedulingDTO.EnterpriseId;
                    schedulingDb.ClientName = schedulingDTO.ClientName;
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
            ISchedulingRepository repository,
            ISchedulingTypeRepository schedulingTypeRepository,
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository,
            IEnterpriseRepository enterpriseRepository
        )
        {
            ValidationContext validationContext = new(scheduling);
            List<ValidationResult> errors = new();
            bool validation = Validator.TryValidateObject(scheduling, validationContext, errors, true);

            messages = errors.Select(erro => new ErrorMessage(erro.MemberNames.FirstOrDefault(), erro.ErrorMessage)).ToList();

            Professional ? professionalDb = professionalRepository.GetById<Professional>(scheduling.ProfessionalId);
            if (professionalDb == null)
            {
                messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
                return false;
            }

            if (scheduling.ClientName.Length < 3)
            {
                messages.Add(new ErrorMessage("Cliente", "Nome do cliente deve ter no mínimo 3 caracteres"));
                validation = false;
            }

            if (Regex.IsMatch(scheduling.ClientName, @"\d"))
            {
                messages.Add(new ErrorMessage("Cliente", "Nome do cliente não pode conter números"));
                validation = false;
            }

            Enterprise ? enterpriseDb = enterpriseRepository.GetById<Enterprise>(scheduling.EnterpriseId);
            if (enterpriseDb == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada"));
                return false;
            }

            SchedulingType ? schedulingTypeDb = schedulingTypeRepository.GetById<SchedulingType>(scheduling.SchedulingTypeId);
            if (schedulingTypeDb == null)
            {
                messages.Add(new ErrorMessage("Tipo de Agendamento", "Tipo de agendamento não encontrado"));
                return false;
            }

            if (schedulingTypeDb.EnterpriseId != scheduling.EnterpriseId)
            {
                messages.Add(new ErrorMessage("Tipo de Agendamento", "Tipo de agendamento não pertence à empresa especificada"));
                return false;
            }

            if (scheduling.ScheduledDate < DateTime.UtcNow)
            {
                messages.Add(new ErrorMessage("Data", "A data agendada não pode ser no passado"));
                return false;
            }

            bool schedulingDb = repository.GetSchedulingByDate(scheduling.ProfessionalId, scheduling.ClientName, scheduling.ScheduledDate);

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
            ISchedulingRepository repository,
            ISchedulingTypeRepository schedulingTypeRepository,
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository,
            IEnterpriseRepository enterpriseRepository
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

            Professional ? professionalDb = professionalRepository.GetById<Professional>(scheduling.ProfessionalId);
            if (professionalDb == null)
            {
                messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
                return false;
            }

            if (scheduling.ClientName.Length < 3)
            {
                messages.Add(new ErrorMessage("Cliente", "Nome do cliente deve ter no mínimo 3 caracteres"));
                validation = false;
            }

            if (Regex.IsMatch(scheduling.ClientName, @"\d"))
            {
                messages.Add(new ErrorMessage("Cliente", "Nome do cliente não pode conter números"));
                validation = false;
            }

            Enterprise ? enterpriseDb = enterpriseRepository.GetById<Enterprise>(scheduling.EnterpriseId);
            if (enterpriseDb == null)
            {
                messages.Add(new ErrorMessage("Empresa", "Empresa não encontrada"));
                return false;
            }

            SchedulingType ? schedulingTypeDb = schedulingTypeRepository.GetById<SchedulingType>(scheduling.SchedulingTypeId);
            if (schedulingTypeDb == null)
            {
                messages.Add(new ErrorMessage("Tipo de Agendamento", "Tipo de agendamento não encontrado"));
                return false;
            }

            if (schedulingTypeDb.EnterpriseId != scheduling.EnterpriseId)
            {
                messages.Add(new ErrorMessage("Tipo de Agendamento", "Tipo de agendamento não pertence à empresa especificada"));
                return false;
            }

            // Verifica se houve alteração em campos que exigem validação de conflito
            bool dataChanged = schedulingDb.ScheduledDate != scheduling.ScheduledDate || 
                               schedulingDb.ProfessionalId != scheduling.ProfessionalId ||
                               schedulingDb.ClientName != scheduling.ClientName;

            // Só valida data no passado se os dados críticos mudaram
            if (dataChanged && scheduling.ScheduledDate < DateTime.UtcNow)
            {
                messages.Add(new ErrorMessage("Data", "A data agendada não pode ser no passado"));
                return false;
            }

            // Só verifica conflitos de horário se os dados críticos mudaram
            if (dataChanged && !repository.IsUnique(scheduling))
            {
                messages.Add(new ErrorMessage("Data", "Já existe um agendamento para este profissional ou cliente nesta data e hora"));
                return false;
            }
            
            return validation;
        }
    }
}
