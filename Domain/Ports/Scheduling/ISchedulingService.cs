using Application.DTOs.Scheduling;
using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Base;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Scheduling
{
    public interface ISchedulingService : IBaseService
    {
        public int GetTodaySchedulings(long id, out List<ErrorMessage> messages);
        public SchedulingStatisticsDTO GetTodaySchedulingsProfessional(long id, out List<ErrorMessage> messages);
        public List<SchedulingDTOOutput> GetSchedulingToday(long id, out List<ErrorMessage> messages);
        public List<SchedulingDTOOutput> GetSchedulingsByPeriod(long id, PeriodType periodType, out List<ErrorMessage> messages);
        public int GetPendentsSchedulings(long id, out List<ErrorMessage> messages);
        public bool Create(SchedulingDTO scheduling, out List<ErrorMessage> messages);
        public void Update(SchedulingDTOUpdate scheduling, out List<ErrorMessage> messages);
    }
}
