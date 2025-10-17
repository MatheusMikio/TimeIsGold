using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ValueObjects;

namespace Domain.DTOs.Enterprise
{
    public class EnterpriseDTOUpdate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public long PlanId { get; set; }
        public Address address { get; set; }
    }
}
