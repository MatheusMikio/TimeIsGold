using Domain.Ports.Base;
using Domain.ValueObjects;

namespace Domain.Ports.Client
{
    public interface IClientRepository : IBaseRepository
    {
        bool EmailExists(string email, long? ignoreId = null);
        bool CpfExists(string cpf, long? ignoreId = null);
        Entities.Client? GetByEmail(string email);
    }
}
