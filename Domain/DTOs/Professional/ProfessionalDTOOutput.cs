using Application.DTOs.Enterprise;
using Domain.DTOs.Base;
using Domain.ValueObjects;

namespace Application.DTOs.Professional
{
    public class ProfessionalDTOOutput : BaseUserDTOOutput
    {
        public ProfessionalType Type { get; set; }
        public long EnterpriseId { get; set; }

        public string Function { get; set; }
        public string About { get; set; }
        public string ActuationTime { get; set; }
        public string CRO { get; set; }
    }
}
