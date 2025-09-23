using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BaseEntities
{
    public abstract class BaseEntityDTO
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime ? ChangedAt { get; set; }
    }
}
