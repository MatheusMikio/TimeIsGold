using Application.DTOs.Enterprise;
using Domain.DTOs.Base;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.Professional
{
    public class ProfessionalDTOOutput : BaseUserDTOOutput
    {
        public ProfessionalType Type { get; set; }
        //[JsonIgnore]
        public EnterpriseDTOOutput Enterprise { get; set; }
    }
}
