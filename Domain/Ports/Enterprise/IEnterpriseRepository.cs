using Domain.DTOs.Enterprise;
using Domain.Ports.Base;
using Domain.Ports.Professional;
using Domain.ValueObjects;

namespace Domain.Ports.Enterprise
{
    public interface IEnterpriseRepository : IBaseRepository
    {
        public bool IsUnique(EnterpriseDTOUpdate enterprise);
        bool CnpjExists(string cnpj);
        Entities.Enterprise? GetByCnpj(string cnpj);
    }
}
