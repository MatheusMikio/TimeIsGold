using Domain.DTOs.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface ISchedulingRepository : IBaseRepository
    {
        public bool IsUnique(PlanDTOUpdate plan);
    }
}
