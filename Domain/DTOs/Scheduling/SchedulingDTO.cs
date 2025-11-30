using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Scheduling
{
    public class SchedulingDTO
    {
        public long ProfessionalId { get; set; }
        public string ClientName { get; set; }
        public DateTime ScheduledDate { get; set; }
        public long EnterpriseId { get; set; }
        public long SchedulingTypeId { get; set; }
    }
}
