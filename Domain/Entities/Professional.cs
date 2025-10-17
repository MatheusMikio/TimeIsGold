using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Professional : BaseUser
    {
        public ProfessionalType Type { get; set; }
        public long EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }

        public string Function { get; set; }

        public Professional() { }
    }
}
