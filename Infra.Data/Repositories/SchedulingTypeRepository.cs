using Application.DTOs.SchedulingType;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
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

        public SchedulingType ? GetByName(SchedulingTypeDTO scheduling) => _context.SchedulingTypes.FirstOrDefault(s => s.Name == scheduling.Name &&
        s.EnterpriseId == scheduling.EnterpriseId);

        public bool IsUnique(SchedulingTypeDTOUpdate plan)
        {
            throw new NotImplementedException();
        }

    }
}
