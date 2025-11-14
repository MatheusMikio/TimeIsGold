using Domain.DTOs.Enterprise;
using Domain.Entities;
using Domain.Ports.Enterprise;
using Infra.Data.Repositories.Base;
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

        public bool CnpjExists(string cnpj)
        {
            return _context.Enterprises.Any(enterprise => enterprise.Cnpj == cnpj);
        }

        public Enterprise ? GetByCnpj(string cnpj)
        {
            return _context.Enterprises.FirstOrDefault(e => e.Cnpj == cnpj);
        }
    }
}
