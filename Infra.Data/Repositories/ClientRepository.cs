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
        public bool EmailExists(string email)
        {
            return _context.Clients.Any(c => c.Email == email);
        }

        public bool CpfExists(string cpf)
        {
            return _context.Clients.Any(c => c.Cpf == cpf);
        }

        public Client? GetByEmail(string email)
    => _context.Clients.FirstOrDefault(p => p.Email == email);
    }
}
