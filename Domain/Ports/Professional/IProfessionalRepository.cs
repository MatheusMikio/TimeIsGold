using Domain.DTOs.Professional;
using Domain.Ports.Base;
using Domain.ValueObjects;
using Domain.Entities;

namespace Domain.Ports.Professional
{
    public interface IProfessionalRepository : IBaseRepository
    {
        bool IsUnique(ProfessionalDTOUpdate professional);
        bool EmailExists(string email);
        bool CpfExists(string cpf);
        Entities.Professional? GetByEmail(string email);
        Entities.Professional? GetByEmailAndType(string email, ProfessionalType type);
    }
}
