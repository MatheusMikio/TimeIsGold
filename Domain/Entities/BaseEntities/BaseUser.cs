using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BaseEntities
{
    public abstract class BaseUser : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public IList<Scheduling> ? Schedulings { get; set; }

        protected BaseUser(){ }
    }
}
