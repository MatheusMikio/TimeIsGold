using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Scheduling
{
    public class SchedulingStatisticsDTO
    {
        public int Total { get; set; }
        public int Pendent { get; set; }
        public int InProgress { get; set; }
        public int Finished { get; set; }
        public int Canceled { get; set; }
    }
}
