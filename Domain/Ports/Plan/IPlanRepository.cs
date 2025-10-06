using Domain.DTOs.Plan;
using Domain.Ports.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Plan
{
    public interface IPlanRepository : IBaseRepository
    {
        public bool IsUnique(PlanDTOUpdate plan);
    }
}
