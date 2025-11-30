using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Scheduling : BaseEntity
    {
        public long ProfessionalId { get; set; }
        public Professional Professional { get; set; }
        public string ClientName { get; set; }
        public long SchedulingTypeId { get; set; }
        public long EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
        public SchedulingType SchedulingType { get; set; }
        public DateTime ScheduledDate { get; set; }
        public Status Status { get; set; }
    }
}
