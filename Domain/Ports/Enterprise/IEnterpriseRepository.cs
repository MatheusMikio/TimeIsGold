using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Enterprise;
using Domain.DTOs.Plan;
using Domain.Ports.Base;

namespace Domain.Ports.Enterprise
{
    public interface IEnterpriseRepository : IBaseRepository
    {
        public bool IsUnique(EnterpriseDTOUpdate enterprise);
    }
}
