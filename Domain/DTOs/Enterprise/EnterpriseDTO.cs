using Domain.ValueObjects;

namespace Application.DTOs.Enterprise
{
    public class EnterpriseDTO
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public long PlanId { get; set; }
        public Address Address { get; set; }
    }
}
