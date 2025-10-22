using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Plan : BaseEntity
    {
        public PlanLevel Level { get; set; }
        public decimal Value { get; set; }
        public int ProfessionalNumberLimit { get; set; }
        public IList<Enterprise> Enterprises { get; set; } = new List<Enterprise>();
    }
}
