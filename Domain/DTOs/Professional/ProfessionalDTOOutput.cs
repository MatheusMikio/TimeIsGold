using Application.DTOs.Enterprise;
using Domain.DTOs.Base;
using Domain.ValueObjects;

namespace Application.DTOs.Professional
{
    public class ProfessionalDTOOutput : BaseUserDTOOutput
    {
        public ProfessionalType Type { get; set; }
        //[JsonIgnore]
        public EnterpriseDTOOutput Enterprise { get; set; }
    }
}
