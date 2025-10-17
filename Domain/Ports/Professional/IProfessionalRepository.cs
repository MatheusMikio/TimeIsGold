using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Plan;
using Domain.DTOs.Professional;
using Domain.Ports.Base;

namespace Domain.Ports.Professional
{
    public interface IProfessionalRepository : IBaseRepository
    {
        public bool IsUnique(ProfessionalDTOUpdate professional);
    }
}
