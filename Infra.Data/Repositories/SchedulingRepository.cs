using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Scheduling;
using Domain.ValueObjects;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class SchedulingRepository : BaseRepository, ISchedulingRepository
    {
        public SchedulingRepository(TimeIsGoldDbContext context) : base(context)
        {
        }

        public bool IsUnique(SchedulingDTOUpdate scheduling)
        {
            DateTime startRange = scheduling.ScheduledDate.AddMinutes(-30);
            DateTime endRange = scheduling.ScheduledDate.AddMinutes(30);

            return !_context.Schedulings.Any(
                s => s.Id != scheduling.Id &&
                (
                    (s.ProfessionalId == scheduling.ProfessionalId || s.ClientId == scheduling.ClientId) &&
                    s.ScheduledDate >= startRange &&
                    s.ScheduledDate < endRange
                )
            );
        }

        public int GetTodaySchedulings(long id)
        {
            DateTime today = DateTime.Today;
            DateTime tomorrow = today.AddDays(1);

            return _context.Schedulings.Count(s => s.EnterpriseId == id &&
                s.ScheduledDate >= today &&
                s.ScheduledDate < tomorrow
            );
        }

        public bool GetSchedulingByDate(long professionalId, long clientId, DateTime scheduledDate)
        {
            DateTime startRange = scheduledDate.AddMinutes(-30);
            DateTime endRange = scheduledDate.AddMinutes(30);

            return _context.Schedulings.Any(
                s => s.ProfessionalId == professionalId &&
                     s.ClientId == clientId &&
                     s.ScheduledDate >= startRange &&
                     s.ScheduledDate < endRange
            );
        }

        public List<Scheduling> GetSchedulingsByPeriod(long id, PeriodType periodType)
        {
            DateTime today = DateTime.Today;
            DateTime startDate = today;
            DateTime endDate = today;

            if (periodType == PeriodType.Week)
            {
                int diff = (int)today.DayOfWeek;
                startDate = today.AddDays(-diff);
                endDate = startDate.AddDays(7);
            }

            if (periodType == PeriodType.Month)
            {
                startDate = new DateTime(today.Year, today.Month, 1);
                endDate = startDate.AddMonths(1);
            }

            return _context.Schedulings
                .Where(s => s.EnterpriseId == id &&
                            s.ScheduledDate >= startDate &&
                            s.ScheduledDate < endDate)
                .ToList();
        }

        public int GetPendentsSchedulings(long id)
        {
            return _context.Schedulings.Count(s => s.Professional.EnterpriseId == id &&
                s.Status == Status.Pendent
            );
        }
    }
}
