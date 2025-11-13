using Domain.Entities;
using Domain.Ports.Base;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        

        public List<Thing> GetByTextFilter<Thing>(int page, int size, string searchText) where Thing : class
        {
            IQueryable<Thing> query = _context.Set<Thing>().AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = typeof(Thing).Name switch
                {
                    nameof(Enterprise) => query
                        .Include("Address")
                        .Where(e =>
                            (e as Enterprise).Name.Contains(searchText) ||
                            (e as Enterprise).Cnpj.Contains(searchText) ||
                            (e as Enterprise).Address.Street.Contains(searchText) ||
                            (e as Enterprise).Address.City.Contains(searchText) ||
                            (e as Enterprise).Address.State.Contains(searchText) ||
                            (e as Enterprise).Address.Country.Contains(searchText)
                        ),

                    nameof(
                        Client) => query.Where(c => EF.Property<string>(c, "Name")
                        .Contains(searchText) ||
                        EF.Property<string>(c, "Cpf").Contains(searchText) ||
                        EF.Property<string>(c, "Email").Contains(searchText)
                    ),

                    nameof(
                        Professional) => query.Where(p => EF.Property<string>(p, "Name")
                        .Contains(searchText) ||
                        EF.Property<string>(p, "Cpf").Contains(searchText) ||
                        EF.Property<string>(p, "Email").Contains(searchText)
                    ),

                    nameof(
                        SchedulingType) => query.Where(st => EF.Property<string>(st, "Name")
                        .Contains(searchText)
                    ),

                    _ => query
                };
            }

            return query
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();
        }

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
