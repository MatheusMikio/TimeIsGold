using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BaseService<TEntity, TRepository> : IBaseService where TEntity : class
        where TRepository : IBaseRepository
    {
        private readonly TRepository _repository;

        public BaseService(TRepository repository)
        {
            _repository = repository;
        }
        
        public List<Thing> GetAll<Thing>(int page, int size)
        {
            List<TEntity> entities = _repository.GetAll<TEntity>(page, size);
            
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
