using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Professional;
using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Base;

namespace Domain.Ports.Professional
{
    public interface IProfessionalService : IBaseService
    {
        public bool Create(ProfessionalDTO professionalDTO, out List<ErrorMessage> messages);
        public void Update(ProfessionalDTOUpdate entity, out List<ErrorMessage> mensagens);
    }
}
