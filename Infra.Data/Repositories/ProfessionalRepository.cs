using Domain.DTOs.Professional;
using Domain.Entities;
using Domain.Ports.Professional;
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
            return _context.Professionals.Any(professional => professional.Cpf == cpf);
        }

        public bool EmailExists(string email)
        {
            return _context.Professionals.Any(professional => professional.Email == email);
        }

        //se email existir e nao for o mesmo do id
        public bool EmailIdExists(string email, long id)
        {
            return _context.Professionals.Any(professional => professional.Email == email && professional.Id != id);
        }

        public Professional? GetById(long id)
        {
            return _context.Professionals.Find(id);
        }

        public List<Professional> GetAll()
        {
            return _context.Professionals.ToList();
        }

        public bool IsUnique(ProfessionalDTOUpdate professional)
        {
            throw new NotImplementedException();
        }

        public bool CpfExists(string cpf, long id)
        {
            throw new NotImplementedException();
        }

        public bool CpfExists(string cpf, int ignoreId)
        {
            throw new NotImplementedException();
        }

        public bool EmailExists(string email, long id)
        {
            throw new NotImplementedException();
        }

        public bool EmailExists(string email, int ignoreId)
        {
            throw new NotImplementedException();
        }
    }
}
