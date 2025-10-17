using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Enterprise;
using Domain.DTOs.Plan;

namespace Domain.Ports.Enterprise
{
    public interface IEnterpriseRepository
    {
        public bool IsUnique(EnterpriseDTOUpdate enterprise);
    }
}
