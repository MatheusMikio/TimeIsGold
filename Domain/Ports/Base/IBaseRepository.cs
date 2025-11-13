using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports.Base
{
    public interface IBaseRepository
    {
        public List<Thing> GetAll<Thing>(int page, int size) where Thing : class;
        public Thing ? GetById<Thing>(long id) where Thing : class;
        public List<Thing> GetByTextFilter<Thing>(int page, int size, string searchText) where Thing : class;
        public void Create<Thing>(Thing entity) where Thing : class;
        public void Update<Thing>(Thing entity) where Thing : class;
        public void Delete<Thing>(Thing entity) where Thing : class;
    }
}
