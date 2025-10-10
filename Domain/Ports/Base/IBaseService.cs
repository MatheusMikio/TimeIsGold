using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Base
{
    public interface IBaseService
    {
        public List<Thing> GetAll<Thing>(int page, int size, string filter = null);
        public Thing GetById<Thing>(long id);
        public bool Delete<Thing>(long id);
    }
}
