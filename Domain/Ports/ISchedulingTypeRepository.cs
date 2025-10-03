using Domain.DTOs.Plan;
using Domain.DTOs.SchedulingType;
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
    }
}
