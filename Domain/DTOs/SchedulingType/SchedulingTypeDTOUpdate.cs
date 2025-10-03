using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.SchedulingType
{
    public class SchedulingTypeDTOUpdate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long EnterpriseId { get; set; }
    }
}
