using Application.DTOs.Client;
using Application.DTOs.Plan;
using Application.DTOs.Professional;
using Application.DTOs.SchedulingType;
using Domain.DTOs.Base;
using Domain.ValueObjects;
using System.Text.Json.Serialization;

namespace Application.DTOs.Enterprise
{
    public class EnterpriseDTOOutput : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public Address Address { get; set; }
        public long PlanId { get; set; }
        public IList<SchedulingTypeDTOOutput> ? SchedulingType { get; set; }
        public IList<ProfessionalDTOOutput> ? Professionals { get; set; }
    }
}
