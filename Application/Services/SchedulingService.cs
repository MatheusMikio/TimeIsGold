using Application.DTOs.Scheduling;
using AutoMapper;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Scheduling;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    //private readonly IClientRepository _clientRepository;
    //private readonly IProfessionalRepository _professionalRepository;
    public class SchedulingService : BaseService<SchedulingDTO, Scheduling, ISchedulingRepository>, ISchedulingService
    {
        public SchedulingService(
            ISchedulingRepository repository,
            IMapper mapper/*
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository*/
        ) : base(
            repository,
            mapper
            )
        {
            /*_clientRepository = clientRepository
             _professionalRepository = professionalRepository*/
        }

        public bool Create(SchedulingDTO schedulingDTO, out List<ErrorMessage> messages)
        {
            bool valid = Validate(schedulingDTO, out messages, _repository/*, _clientRepository, _professionalRepository*/);

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
            bool valid = ValidateUpdate(schedulingDTO, out messages, _repository/*, _clientRepository, _professionalRepository*/);

            if (valid)
            {
                Scheduling ? schedulingDb = _repository.GetById<Scheduling>(schedulingDTO.Id);
                if (schedulingDb == null)
                {
                    messages.Add(new ErrorMessage("Agendamento", "Agendamento não encontrado"));
                    return;
                }

                /*Professional ? professionalDb = _professionalRepository.GetById<Professional>(scheduling.ProfessionalId);
                if (professionalDb == null)
                {
                    messages.Add(new ErrorMessage("Profissional", "Profissional não encontrado"));
                    return;
                }

                Client ? clientDb = _clientRepository.GetById<Client>(scheduling.ClientId);
                if (clientDb == null)
                {
                    messages.Add(new ErrorMessage("Cliente", "Cliente não encontrado"));
                    return;
                }*/


                try
                {
                    //schedulingDb.Client = clientDb;
                    //schedulingDb.ClientId = schedulingDTO.ClientId;
                    //schedulingDb.Professional = professionalDb;
                    //schedulingDb.ProfessionalId = schedulingDTO.ProfessionalId;
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
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository*/
        )
        {
            throw new NotImplementedException();
        }

        public static bool ValidateUpdate(
            SchedulingDTOUpdate scheduling,
            out List<ErrorMessage> messages,
            ISchedulingRepository repository/*,
            IClientRepository clientRepository,
            IProfessionalRepository professionalRepository*/
        )
        {
            throw new NotImplementedException();
        }
    }
}
