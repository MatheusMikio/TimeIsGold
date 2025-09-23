using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Enterprise
{
    public class EnterpriseDTO
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public long PlanId { get; set; }
        public Address Address { get; set; }
    }
}
