using Application.DTOs.Enterprise;
using Application.DTOs.Scheduling;
using Domain.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Client
{
    public class ClientDTOOutput : BaseUserDTOOutput
    {
        public IList<EnterpriseDTOOutput> ? Enterprises { get; set; }
    }
}
