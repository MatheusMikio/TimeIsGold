using Domain.Ports.Base;
using Domain.Entities;

namespace Domain.Ports.Client
{
    public interface IClientRepository : IBaseRepository
    {
        bool EmailExists(string email, long? ignoreId = null);
        bool CpfExists(string cpf, long? ignoreId = null);
        Entities.Client GetById(long id);
        List<Entities.Client> GetAll();
    }
}
