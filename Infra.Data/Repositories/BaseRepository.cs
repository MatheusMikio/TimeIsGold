using Domain.Ports;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly TimeIsGoldDbContext _context;

        public BaseRepository(TimeIsGoldDbContext context)
        {
            _context = context;
        }

        public List<Thing> GetAll<Thing>(int page, int size) where Thing : class
            => _context.Set<Thing>()
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();

        public Thing ? GetById<Thing>(long id) where Thing : class => _context.Set<Thing>().Find(id);

        public void Create<Thing>(Thing entity) where Thing : class
        {
            _context.Set<Thing>().Add(entity);
            _context.SaveChanges();
        }

        public void Update<Thing>(Thing entity) where Thing : class
        {
            _context.Set<Thing>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete<Thing>(Thing entity) where Thing : class
        {
            _context.Set<Thing>().Remove(entity);
            _context.SaveChanges();
        }   
    }
}
