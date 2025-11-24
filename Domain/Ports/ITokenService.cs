using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface ITokenService
    {
        public string Generate(ProfessionalDTOOutput professional);
    }
}
