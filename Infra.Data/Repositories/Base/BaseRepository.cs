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

namespace Infra.Data.Repositories.Base
{
    public class BaseRepository : IBaseRepository
    {
        protected readonly TimeIsGoldDbContext _context;

        public BaseRepository(TimeIsGoldDbContext context)
        {
            _context = context;
        }

        public List<Thing> GetAll<Thing>(int page, int size) where Thing : class
        {
            IQueryable<Thing> query = _context.Set<Thing>();

            if (typeof(Thing) == typeof(Enterprise))
            {
                query = query
                    .Include("SchedulingType")
                    .Include("Professionals");
            }

            if (typeof(Thing) == typeof(Client))
            {
                query = query
                    .Include("Schedulings");
            }

            if (typeof(Thing) == typeof(Scheduling))
            {
                query = query
                    .Include("Professional")
                    .Include("Client")
                    .Include("SchedulingType")
                    .Include("Enterprise");
            }

            return query
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();
        }
        public List<Thing> GetByTextFilter<Thing>(int page, int size, string searchText) where Thing : class
        {
            IQueryable<Thing> query = _context.Set<Thing>().AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = typeof(Thing).Name switch
                {
                    nameof(Enterprise) => query
                        .Include("Address")
                        .Include("SchedulingType")
                        .Include("Professionals")
                        .Where(e =>
                            (e as Enterprise).Name.Contains(searchText) ||
                            (e as Enterprise).Cnpj.Contains(searchText) 
                        ),

                    nameof(
                        Client) => query
                        .Include("Schedulings")
                        .Where(c => EF.Property<string>(c, "Name")
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

                    nameof(
                        Scheduling) => query
                        .Include("Professional")
                        .Include("Client")
                        .Include("SchedulingType")
                        .Where(s => 
                            EF.Property<string>(s, "Status").Contains(searchText)
                        ),

                    _ => query
                };
            }

            return query
                .Skip((page - 1) * size)
                .Take(size)
                .ToList();
        }

        public Thing ? GetById<Thing>(long id) where Thing : class
        {
            if (typeof(Thing) == typeof(Enterprise))
            {
                return _context.Set<Thing>()
                    .Include("SchedulingType")
                    .Include("Professionals")
                    .FirstOrDefault(e => EF.Property<long>(e, "Id") == id);
            }

            if (typeof(Thing) == typeof(Client))
            {
                return _context.Set<Thing>()
                    .Include("Schedulings")
                    .FirstOrDefault(c => EF.Property<long>(c, "Id") == id);
            }

            if (typeof(Thing) == typeof(Scheduling))
            {
                return _context.Set<Thing>()
                    .Include("Professional")
                    .Include("Client")
                    .Include("SchedulingType")
                    .FirstOrDefault(s => EF.Property<long>(s, "Id") == id);
            }

            return _context.Set<Thing>().Find(id);
        }

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
