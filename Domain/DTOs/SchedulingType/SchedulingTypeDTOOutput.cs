using Application.DTOs.Enterprise;
using Domain.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SchedulingType
{
    public class SchedulingTypeDTOOutput : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public long EnterpriseId { get; set; }
    }
}
