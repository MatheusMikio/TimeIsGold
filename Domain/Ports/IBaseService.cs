using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Ports
{
    public interface IBaseService
    {
        public List<Thing> GetAll<Thing>(int page, int size);
        public List<Thing> Get<Thing>(string q);
        public Thing GetById<Thing>(long id);
        public bool Create<TCreateDTO>(TCreateDTO entity, out List<ErrorMessage> erros);
        public void Update<TUpdateDTO>(TUpdateDTO entity, out List<ErrorMessage> erros);
        public bool Delete<Thing>(long id);
    }
}
