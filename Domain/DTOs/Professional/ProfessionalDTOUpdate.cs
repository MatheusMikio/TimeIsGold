using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Professional
{
    public class ProfessionalDTOUpdate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long EnterpriseId { get; set; }
        public int Type { get; set; }
        public string Function { get; set; }
    }
}
