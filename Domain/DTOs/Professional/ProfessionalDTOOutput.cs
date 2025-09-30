using Application.DTOs.Enterprise;
using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Professional
{
    public class ProfessionalDTOOutput : BaseUserDTOOutput
    {
        public ProfessionalType Type { get; set; }
        public EnterpriseDTOOutput Enterprise { get; set; }
    }
}
