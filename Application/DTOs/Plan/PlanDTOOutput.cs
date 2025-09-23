using Application.DTOs.Enterprise;
using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Plan
{
    public class PlanDTOOutput : BaseEntityDTO
    {
        public PlanLevel Level { get; set; }
        public decimal value { get; set; }
        public int ScheduleTypeLimit { get; set; }
        public IList<EnterpriseDTOOutput> ? Enterprises { get; set; }
    }
}
