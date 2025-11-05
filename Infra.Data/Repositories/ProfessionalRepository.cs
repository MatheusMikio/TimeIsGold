using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Professional;
using Domain.ValueObjects;
using Infrastructure.Data;

namespace Infra.Data.Repositories
{
    public class ProfessionalRepository : BaseRepository, IProfessionalRepository
    {
        public ProfessionalRepository(TimeIsGoldDbContext context) : base(context)
        {
        }

        public bool CpfExists(string cpf, long? ignoreId = null)
        {
            return ignoreId.HasValue
                ? _context.Professionals.Any(p => p.Cpf == cpf && p.Id != ignoreId.Value)
                : _context.Professionals.Any(p => p.Cpf == cpf);
        }

        public bool EmailExists(string email, long? ignoreId = null)
        {
            return ignoreId.HasValue
                ? _context.Professionals.Any(p => p.Email == email && p.Id != ignoreId.Value)
                : _context.Professionals.Any(p => p.Email == email);
        }

        public bool IsUnique(ProfessionalDTOUpdate professional)
        {
            throw new NotImplementedException();
        }

        public Professional? GetByEmail(string email)
            => _context.Professionals.FirstOrDefault(p => p.Email == email);

        public Professional? GetByEmailAndType(string email, ProfessionalType type)
            => _context.Professionals.FirstOrDefault(p => p.Email == email && p.Type == type);
    }
}
