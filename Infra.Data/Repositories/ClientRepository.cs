using Domain.Entities;
using Domain.Ports.Client;
using Infrastructure.Data;
using System.Linq;
using System.Collections.Generic;
using Infra.Data.Repositories;

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
        public Client? GetById(long id)
        {
            return _context.Clients.Find(id);
        }

        public List<Client> GetAll()
        {
            return _context.Clients.ToList();
        }
    }
}
