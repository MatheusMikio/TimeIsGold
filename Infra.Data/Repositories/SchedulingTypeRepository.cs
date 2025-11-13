using Application.DTOs.SchedulingType;
using Domain.DTOs.SchedulingType;
using Domain.Entities;
using Domain.Ports.SchedulingType;
using Infra.Data.Repositories.Base;
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
            
        public bool GetByName(string schedulingTypeName, long enterpriseId) => _context.
            SchedulingTypes.Any(s => s.Name == schedulingTypeName &&
            s.EnterpriseId == enterpriseId);

        public bool IsUnique(SchedulingTypeDTOUpdate schedulingType)
        {
            return !_context.SchedulingTypes.Any(
                s => s.Id != schedulingType.Id &&
                (
                    s.Name == schedulingType.Name &&
                    s.EnterpriseId == schedulingType.EnterpriseId
                )
            );
        }

    }
}
