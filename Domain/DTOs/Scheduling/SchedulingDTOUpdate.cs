using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Scheduling
{
    public class SchedulingDTOUpdate
    {
        public long Id { get; set; }
        public long ProfessionalId { get; set; }
        public long ClientId { get; set; }
        public long SchedulingTypeId { get; set; }
        public DateTime ScheduledDate { get; set; }
        public int Status { get; set; }
    }
}
