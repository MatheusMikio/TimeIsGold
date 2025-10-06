using Domain.DTOs.Plan;
using Domain.Ports;
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

        public bool IsUnique(PlanDTOUpdate plan)
        {
            throw new NotImplementedException();
        }
    }
}
