using Application.DTOs.Client;
using Application.DTOs.Plan;
using Domain.Entities.BaseEntities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Enterprise
{
    public class EnterpriseDTOOutput : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public Address Address { get; set; }
        public PlanDTOOutput ? Plan { get; set; }
        public IList<ClientDTOOutput> ? Clients { get; set; }
    }
}
