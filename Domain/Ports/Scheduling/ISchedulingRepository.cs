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
        public bool GetSchedulingByDate(long professionalId, long clientId, DateTime scheduledDate);
        public List<Entities.Scheduling> GetSchedulingsByPeriod(long id, PeriodType periodType);
        public int GetPendentsSchedulings(long id);
        public bool IsUnique(SchedulingDTOUpdate plan);
    }
}
