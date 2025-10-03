using Application.DTOs.Scheduling;
using Application.DTOs.SchedulingType;
using AutoMapper;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
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
            throw new NotImplementedException();
        }

        public void Update(SchedulingTypeDTOUpdate entity, out List<ErrorMessage> mensagens)
        {
            throw new NotImplementedException();
        }
    }
}
