using Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SchedulingType : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public long EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
    }
}
