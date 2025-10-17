using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Enterprise;
using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Base;

namespace Domain.Ports.Enterprise
{
    public interface IEnterpriseService
    {
        public interface IEnterpriseService : IBaseService
        {
            public bool Create(EnterpriseDTO enterpriseDTO, out List<ErrorMessage> messages);
            public void Update(EnterpriseDTOUpdate entity, out List<ErrorMessage> mensagens);
        }
    }
}
