using Application.DTOs.Scheduling;
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
    public interface ISchedulingTypeRepository : IBaseRepository
    {
        public bool IsUnique(SchedulingTypeDTOUpdate plan);
        public SchedulingType GetByName(SchedulingTypeDTO name);
    }
}
