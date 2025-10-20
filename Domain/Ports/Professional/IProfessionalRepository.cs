using Domain.DTOs.Professional;
using Domain.Ports.Base;

namespace Domain.Ports.Professional
{
    public interface IProfessionalRepository : IBaseRepository
    {
        public bool IsUnique(ProfessionalDTOUpdate professional);
        bool CpfExists(string cpf, long id);
        bool CpfExists(string cpf, int ignoreId);
        bool EmailExists(string email, long id);
        bool EmailExists(string email, int ignoreId);
        bool CpfExists(string cpf);
        bool EmailExists(string email);
    }
}
