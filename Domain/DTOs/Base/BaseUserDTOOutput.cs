using Application.DTOs.Scheduling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Base
{
    public abstract class BaseUserDTOOutput : BaseEntityDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }  
        public bool Status { get; set; }
        public IList<SchedulingDTOOutput> ? Schedulings { get; set; }
    }
}
