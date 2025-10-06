using Application.DTOs.Plan;
using Application.DTOs.SchedulingType;
using Domain.DTOs.Plan;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface ISchedulingTypeService : IBaseService
    {
        public bool Create(SchedulingTypeDTO planDTO, out List<ErrorMessage> messages);
        public void Update(SchedulingTypeDTOUpdate entity, out List<ErrorMessage> mensagens);
    }
}
