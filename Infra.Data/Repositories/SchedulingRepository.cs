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
            return !_context.Schedulings.Any(
               s => s.Id != scheduling.Id &&
               (
                   s.ProfessionalId == scheduling.ProfessionalId &&
                   s.ClientId == scheduling.ClientId &&
                   s.ScheduledDate == scheduling.ScheduledDate
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

        public List<Scheduling> GetWeekSchedulings(long id)
        {
            DateTime today = DateTime.Today;
            DateTime week = today.AddDays(8);

            return _context.Schedulings.Where(s => s.EnterpriseId == id &&
                s.ScheduledDate >= today &&
                s.ScheduledDate < week
            ).ToList();
        }

        public List<Scheduling> GetMonthSchedulings(long id)
        {
            DateTime today = DateTime.Today;
            DateTime month = today.AddDays(31);
            return _context.Schedulings.Where(s => s.EnterpriseId == id &&
                s.ScheduledDate >= today &&
                s.ScheduledDate < month
            ).ToList();
        }

        public int GetPendentsSchedulings(long id)
        {
            return _context.Schedulings.Count(s => s.Professional.EnterpriseId == id &&
                s.Status == Status.Pendent
            );
        }
    }
}
