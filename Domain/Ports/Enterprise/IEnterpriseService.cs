using Application.DTOs.Enterprise;
using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Base;

namespace Domain.Ports.Enterprise
{
    public interface IEnterpriseService : IBaseService
    {
        Entities.Enterprise? GetById(long id);
        Entities.Enterprise? Create(EnterpriseDTO dto, out List<ErrorMessage> messages);
        void Update(EnterpriseDTOUpdate entity, out List<ErrorMessage> messages);


    }
}