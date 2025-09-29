using AutoMapper;
using Domain.Entities;
using Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BaseService<TDTO, TEntity, TRepository> : IBaseService 
        where TDTO : class 
        where TEntity : class
        where TRepository : IBaseRepository
    {
        protected readonly TRepository _repository;
        protected readonly IMapper _mapper;

        public BaseService(TRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public List<Thing> GetAll<Thing>(int page, int size)
        {
            List<TEntity> entities = _repository.GetAll<TEntity>(page, size);
            return _mapper.Map<List<Thing>>(entities);
        }

        public List<Thing> Get<Thing>(string q)
        {
            throw new NotImplementedException();
        }

        public Thing GetById<Thing>(long id)
        {
            TEntity ? entity = _repository.GetById<TEntity>(id);

            if (entity == null) return default;

            return _mapper.Map<Thing>(entity);
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
