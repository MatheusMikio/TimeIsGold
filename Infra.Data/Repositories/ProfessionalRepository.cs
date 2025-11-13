using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Professional;
using Domain.ValueObjects;
using Infra.Data.Repositories.Base;
using Infrastructure.Data;

namespace Infra.Data.Repositories
{
    public class ProfessionalRepository : BaseRepository, IProfessionalRepository
    {
        public ProfessionalRepository(TimeIsGoldDbContext context) : base(context)
        {
        }

        public bool CpfExists(string cpf)
        {
            return _context.Professionals.Any(p => p.Cpf == cpf);
        }

        public bool EmailExists(string email)
        {
            return _context.Professionals.Any(p => p.Email == email);
        }

        public bool IsUnique(ProfessionalDTOUpdate professional)
        {
            return !_context.Professionals.Any(p =>
                (p.Email == professional.Email || p.Cpf == professional.Cpf)
                && p.Id != professional.Id);
        }

        public Professional? GetByEmail(string email)
            => _context.Professionals.FirstOrDefault(p => p.Email == email);

        public Professional? GetByEmailAndType(string email, ProfessionalType type)
            => _context.Professionals.FirstOrDefault(p => p.Email == email && p.Type == type);
    }
}
