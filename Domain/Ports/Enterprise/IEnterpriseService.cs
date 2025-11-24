using Application.DTOs.Enterprise;
using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Base;

namespace Domain.Ports.Enterprise
{
    public interface IEnterpriseService : IBaseService
    {
        
        public bool Create(EnterpriseDTO dto, out List<ErrorMessage> messages);
        public void Update(EnterpriseDTOUpdate entity, out List<ErrorMessage> messages);
    }
}