using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Professional
{
    public class ProfessionalDTO
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long EnterpriseId { get; set; }
        public int ProfessionalTypeId { get; set; }
    }
}
