using Domain.DTOs.Scheduling;
using Domain.Entities;
using Domain.Ports.Scheduling;
using Domain.ValueObjects;
using Infra.Data.Repositories.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
            DateTime startRange = DateTime.SpecifyKind(scheduling.ScheduledDate.AddMinutes(-30), DateTimeKind.Utc);
            DateTime endRange = DateTime.SpecifyKind(scheduling.ScheduledDate.AddMinutes(30), DateTimeKind.Utc);

            return !_context.Schedulings.Any(
                s => s.Id != scheduling.Id &&
                (
                    (s.ProfessionalId == scheduling.ProfessionalId || s.ClientName == scheduling.ClientName) &&
                    s.ScheduledDate >= startRange &&
                    s.ScheduledDate < endRange
                )
            );
        }

        public int GetTodaySchedulings(long id)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime tomorrow = today.AddDays(1);

            return _context.Schedulings.Count(s => s.EnterpriseId == id &&
                s.ScheduledDate >= today &&
                s.ScheduledDate < tomorrow
            );
        }

        public List<Scheduling> GetSchedulingsProfessional(long id)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime tomorrow = today.AddDays(1);
            return _context.Schedulings
                .Include(s => s.Professional)
                .Include(s => s.SchedulingType)
                .Where(s => s.ProfessionalId == id &&
                            s.ScheduledDate >= today &&
                            s.ScheduledDate < tomorrow)
                .ToList();
        }

        public Dictionary<Status, int> GetTodaySchedulingsProfessional(long id)
        {
            DateTime today = DateTime.UtcNow.Date;
            DateTime tomorrow = today.AddDays(1);

            var schedulings = _context.Schedulings
                .Where(s => s.ProfessionalId == id &&
                    s.ScheduledDate >= today &&
                    s.ScheduledDate < tomorrow)
                .ToList();

            var grouped = schedulings
                .GroupBy(s => s.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new Dictionary<Status, int>
            {
                { Status.Pendent, grouped.ContainsKey(Status.Pendent) ? grouped[Status.Pendent] : 0 },
                { Status.InProgress, grouped.ContainsKey(Status.InProgress) ? grouped[Status.InProgress] : 0 },
                { Status.Finished, grouped.ContainsKey(Status.Finished) ? grouped[Status.Finished] : 0 },
                { Status.Canceled, grouped.ContainsKey(Status.Canceled) ? grouped[Status.Canceled] : 0 }
            };

            result.Add((Status)0, schedulings.Count);

            return result;
        }

        public bool GetSchedulingByDate(long professionalId, string ClientName, DateTime scheduledDate)
        {
            DateTime startRange = DateTime.SpecifyKind(scheduledDate.AddMinutes(-30), DateTimeKind.Utc);
            DateTime endRange = DateTime.SpecifyKind(scheduledDate.AddMinutes(30), DateTimeKind.Utc);

            return _context.Schedulings.Any(
                s => s.ProfessionalId == professionalId &&
                     s.ClientName == ClientName &&
                     s.ScheduledDate >= startRange &&
                     s.ScheduledDate < endRange
            );
        }


        public bool GetSchedulingByDate(long professionalId, DateTime scheduledDate)
        {
            DateTime startRange = DateTime.SpecifyKind(scheduledDate.AddMinutes(-30), DateTimeKind.Utc);
            DateTime endRange = DateTime.SpecifyKind(scheduledDate.AddMinutes(30), DateTimeKind.Utc);

            return _context.Schedulings.Any(
                s => s.ProfessionalId == professionalId &&
                     s.ScheduledDate >= startRange &&
                     s.ScheduledDate < endRange
            );
        }

        public List<Scheduling> GetSchedulingsByPeriod(long id, PeriodType periodType)
        {
            DateTime today = DateTime.UtcNow.Date;
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
                startDate = new DateTime(today.Year, today.Month, 1, 0, 0, 0, DateTimeKind.Utc);
                endDate = startDate.AddMonths(1);
            }

            return _context.Schedulings
                .Include(s => s.Professional)
                .Include(s => s.SchedulingType)
                .Where(s => s.ProfessionalId == id &&
                            s.ScheduledDate >= startDate &&
                            s.ScheduledDate < endDate)
                .ToList();
        }

        public int GetPendentsSchedulings(long id)
        {
            return _context.Schedulings.Count(s => s.EnterpriseId == id &&
                s.Status == Status.Pendent
            );
        }
    }
}
