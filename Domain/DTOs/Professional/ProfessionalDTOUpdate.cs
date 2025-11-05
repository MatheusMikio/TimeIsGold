namespace Domain.DTOs.Professional
{
    public class ProfessionalDTOUpdate
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long EnterpriseId { get; set; }
        public int Type { get; set; }
        public string Function { get; set; }
        public string Phone { get; set; }
        public string? About { get; set; }
        public string ActuationTime { get; set; }
    }
}
