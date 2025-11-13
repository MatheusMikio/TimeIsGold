using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.SchedulingType
{
    public class SchedulingTypeDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public long EnterpriseId { get; set; }
    }
}
