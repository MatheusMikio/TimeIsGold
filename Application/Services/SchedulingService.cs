using Application.DTOs.Scheduling;
using AutoMapper;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SchedulingService : BaseService<SchedulingDTO, Scheduling, ISchedulingRepository>, ISchedulingService
    {
        public SchedulingService(ISchedulingRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public bool Create(SchedulingDTO scheduling, out List<ErrorMessage> messages)
        {
            throw new NotImplementedException();
        }

        public void Update(SchedulingDTOUpdate scheduling, out List<ErrorMessage> messages)
        {
            throw new NotImplementedException();
        }
    }
}
