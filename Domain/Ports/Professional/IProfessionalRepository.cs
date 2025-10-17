using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Plan;
using Domain.DTOs.Professional;

namespace Domain.Ports.Professional
{
    public interface IProfessionalRepository
    {
        public bool IsUnique(ProfessionalDTOUpdate professional);
    }
}
