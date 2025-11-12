using Domain.Ports.Base;
using Domain.ValueObjects;

namespace Domain.Ports.Client
{
    public interface IClientRepository : IBaseRepository
    {
        bool EmailExists(string email);
        bool CpfExists(string cpf);
        Entities.Client? GetByEmail(string email);
    }
}
