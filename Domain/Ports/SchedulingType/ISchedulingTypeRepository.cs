using Application.DTOs.Scheduling;
using Application.DTOs.SchedulingType;
using Domain.DTOs.Plan;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
using Domain.Ports.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.SchedulingType
{
    public interface ISchedulingTypeRepository : IBaseRepository
    {
        public bool IsUnique(SchedulingTypeDTOUpdate plan);
        public bool GetByName(string name, long enterpriseId);
    }
}
