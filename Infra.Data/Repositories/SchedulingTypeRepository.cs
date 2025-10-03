using Domain.DTOs.SchedulingType;
using Domain.Ports;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class SchedulingTypeRepository : BaseRepository, ISchedulingTypeRepository
    {
        public SchedulingTypeRepository(TimeIsGoldDbContext context) : base(context)
        {
        }

        public bool IsUnique(SchedulingTypeDTOUpdate plan)
        {
            throw new NotImplementedException();
        }
    }
}
