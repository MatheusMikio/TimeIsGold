namespace Domain.Entities.BaseEntities
{
    public abstract class BaseUser : BaseEntity
    {
        public long Id { get; protected set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public IList<Scheduling> ? Schedulings { get; set; }

        protected BaseUser(){ }
    }
}
