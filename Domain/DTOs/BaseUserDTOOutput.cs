using Application.DTOs.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BaseEntities
{
    public abstract class BaseUserDTOOutput : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public IList<SchedulingDTOOutput> ? Schedulings { get; set; }
    }
}
