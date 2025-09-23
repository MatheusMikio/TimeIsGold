using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BaseService : IBaseService
    {
        public List<Thing> GetAll<Thing>(int page, int size)
        {
            throw new NotImplementedException();
        }
        public List<Thing> Get<Thing>(string q)
        {
            throw new NotImplementedException();
        }

        public Thing GetById<Thing>(long id)
        {
            throw new NotImplementedException();
        }

        public bool Create<Thing>(Thing entity, out List<ErrorMessage> mensagens)
        {
            throw new NotImplementedException();
        }

        public void Update<Thing>(Thing entity, out List<ErrorMessage> mensagens)
        {
            throw new NotImplementedException();
        }

        public bool Delete<Thing>(long id)
        {
            throw new NotImplementedException();
        }
    }
}
