using Domain.Entities.BaseEntities;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Enterprise : BaseEntity
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public long PlanId { get; set; }
        public Plan Plan { get; set; }
        public Address Address { get; set; }
        public IList<SchedulingType> ? SchedulingType { get; set; }
        public IList<Professional> ? Professionals { get; set; }
        public IList<Client> ? Clients { get; set; }
    }
}


