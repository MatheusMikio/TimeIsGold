using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Enterprise;
using Infrastructure.Data;

namespace Infra.Data.Repositories
{
    public class EnterpriseRepository : BaseRepository, IEnterpriseRepository
    {
        public EnterpriseRepository(TimeIsGoldDbContext context) : base(context)
        {
        }

        public bool IsUnique(EnterpriseDTOUpdate enterprise)
        {
            return !_context.Enterprises.Any(ent =>
                (ent.Cnpj == enterprise.Cnpj || ent.Name == enterprise.Name) &&
                ent.Id != enterprise.Id);
        }

        public bool CnpjExists(string cnpj, long? ignoreId = null)
        {
            return ignoreId.HasValue
                ? _context.Enterprises.Any(enterprise => enterprise.Cnpj == cnpj && enterprise.Id != ignoreId.Value)
                : _context.Enterprises.Any(enterprise => enterprise.Cnpj == cnpj);
        }

        public Enterprise? GetById(long id)
        {
            return _context.Enterprises.Find(id);
        }

        public List<Enterprise> GetAll()
        {
            return _context.Enterprises.ToList();
        }
    }
}
