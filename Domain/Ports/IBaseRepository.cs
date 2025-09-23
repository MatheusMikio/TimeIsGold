using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IBaseRepository
    {
        public List<Thing> GetAll<Thing>(int page, int size) where Thing : class;
        public Thing ? GetById<Thing>(long id) where Thing : class;
        public void Create<Thing>(Thing entity) where Thing : class;
        public void Update<Thing>(Thing entity) where Thing : class;
        public void Delete<Thing>(Thing entity) where Thing : class;
    }
}
