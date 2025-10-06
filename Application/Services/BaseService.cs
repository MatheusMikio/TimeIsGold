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

        public bool Delete<Thing>(long id)
        {
            TEntity ? entity = _repository.GetById<TEntity>(id);

            if (entity == null) return false;

            _repository.Delete(entity);

            return true;
        }

        public virtual bool Create<TCreateDTO>(TCreateDTO entity, out List<ErrorMessage> errors)
        {
            errors = new List<ErrorMessage>();

            try
            {
                TEntity mappedEntity = _mapper.Map<TEntity>(entity);
                _repository.Create(mappedEntity);
                return true;
            }
            catch (Exception ex)
            {
                errors.Add(new ErrorMessage("Sistema","Erro inesperado."));
                return false;
            }
        }

        public virtual void Update<TUpdateDTO>(TUpdateDTO entity, out List<ErrorMessage> errors)
        {
            errors = new List<ErrorMessage>();

            try
            {
                TEntity mappedEntity = _mapper.Map<TEntity>(entity);
                _repository.Update(mappedEntity);
            }
            catch (Exception ex)
            {
                errors.Add(new ErrorMessage("Sistema", "Erro inesperado."));
            }
        }
    }
}
