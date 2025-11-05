using Domain.Entities.BaseEntities;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Professional : BaseUser
    {
        public ProfessionalType Type { get; set; }
        public long EnterpriseId { get; set; }
        public Enterprise Enterprise { get; set; }
        public string Function { get; set; }
        public string? About { get; set; }
        public string ActuationTime { get; set; } 

        public Professional() { }
    }
}
