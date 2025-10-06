using Domain.DTOs.Plan;
using Domain.Ports.Plan;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class PlanRepository : BaseRepository, IPlanRepository
    {
        public PlanRepository(TimeIsGoldDbContext context) : base(context)
        {
        }

        public bool IsUnique(PlanDTOUpdate plan) => !_context.Plans.Any(
            p => p.Id != plan.Id && 
            (
                (int)p.Level == plan.Level
                || p.Value == plan.Value
                || p.ScheduleTypeLimit == plan.ScheduleTypeLimit
            )
        );

        public bool GetLevel(int level) => _context.Plans.Any(p => (int)p.Level == level);
    }
}
