using Application.DTOs.Plan;
using Domain.DTOs.Plan;
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
        public void Update(PlanDTOUpdate entity, out List<ErrorMessage> mensagens);
    }
}
