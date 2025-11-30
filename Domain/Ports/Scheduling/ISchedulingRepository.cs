using Domain.DTOs.Scheduling;
using Domain.Ports.Base;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Scheduling
{
    public interface ISchedulingRepository : IBaseRepository
    {
        public int GetTodaySchedulings(long id);
        public Dictionary<Status, int> GetTodaySchedulingsProfessional(long id);
        public List<Entities.Scheduling> GetSchedulingsByPeriod(long id, PeriodType periodType);
        public List<Entities.Scheduling> GetSchedulingsProfessional(long id);
        public int GetPendentsSchedulings(long id);
        public bool GetSchedulingByDate(long professionalId, string ClientName, DateTime scheduledDate);
        public bool IsUnique(SchedulingDTOUpdate plan);
    }
}
