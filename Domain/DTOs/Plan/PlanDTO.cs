using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Plan
{
    public class PlanDTO
    {
        public int Level { get; set; }
        public decimal Value { get; set; }
        public int ScheduleTypeLimit { get; set; }
    }
}
