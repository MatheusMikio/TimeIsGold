using Application.DTOs.Enterprise;
using Domain.DTOs.Base;
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
        public int Level { get; set; }
        public decimal Value { get; set; }
        public int ProfessionalNumberLimit { get; set; }
        public IList<EnterpriseDTOOutput> ? Enterprises { get; set; }
    }
}
