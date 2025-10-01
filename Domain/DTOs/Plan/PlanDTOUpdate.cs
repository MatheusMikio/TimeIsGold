using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Plan
{
    public class PlanDTOUpdate
    {
        public long Id { get; set; }
        public int Level { get; set; }
        public decimal Value { get; set; }
        public int ScheduleTypeLimit { get; set; }
    }
}
