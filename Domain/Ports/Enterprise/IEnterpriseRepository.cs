using Domain.DTOs.Enterprise;
using Domain.Ports.Base;

namespace Domain.Ports.Enterprise
{
    public interface IEnterpriseRepository : IBaseRepository
    {
        public bool IsUnique(EnterpriseDTOUpdate enterprise);
        bool CnpjExists(string cnpj, long? ignoreId = null);
        Entities.Enterprise? GetById(long id);
        List<Entities.Enterprise> GetAll();
    }
}
