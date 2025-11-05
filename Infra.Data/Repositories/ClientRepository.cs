using Domain.Entities;
using Domain.Ports.Client;
using Domain.ValueObjects;
using Infrastructure.Data;

namespace Infra.Data.Repositories
{
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(TimeIsGoldDbContext context) : base(context)
        {
        }
        public bool EmailExists(string email, long? ignoreId = null)
        {
            return ignoreId.HasValue
                ? _context.Clients.Any(client => client.Email == email && client.Id != ignoreId.Value)
                : _context.Clients.Any(client => client.Email == email);
        }

        public bool CpfExists(string cpf, long? ignoreId = null)
        {
            return ignoreId.HasValue
                ? _context.Clients.Any(client => client.Cpf == cpf && client.Id != ignoreId.Value)
                : _context.Clients.Any(client => client.Cpf == cpf);
        }

        public Client? GetByEmail(string email)
    => _context.Clients.FirstOrDefault(p => p.Email == email);
    }
}
