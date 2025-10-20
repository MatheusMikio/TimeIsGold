using Domain.DTOs.Scheduling;
using Domain.Ports.Scheduling;
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
    }
}
