using Application.DTOs.Plan;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IPlanService : IBaseService
    {
        public bool Create(PlanDTO planDTO, out List<ErrorMessage> messages);
    }
}
