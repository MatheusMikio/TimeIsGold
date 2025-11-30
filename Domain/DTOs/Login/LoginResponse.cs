using Application.DTOs.Professional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Login
{
    public class LoginResponse
    {
        public ProfessionalDTOOutput Professional { get; set; }
        public string Token { get; set; }
    }
}
