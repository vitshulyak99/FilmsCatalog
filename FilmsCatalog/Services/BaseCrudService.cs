using System.Collections.Generic;
using System.Linq;
using FilmsCatalog.Models.Db;
using FilmsCatalog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalog.Services
{
    public abstract class BaseCrudService<T> : ICrudService<T> where T : BaseEntity
    {
        protected BaseCrudService(DbContext context)
        {
            Context = context;
            Set = Context.Set<T>();
        }

        protected DbContext Context { get; }
        protected DbSet<T> Set { get; }

        public int Count => Set.Count();

        public virtual IQueryable<T> Query() => Include(Set);

        public IEnumerable<T> GetAll() => Query().ToList();

        public virtual T GetById(int id) => Include(Set.Where(x => x.Id.Equals(id))).FirstOrDefault();

        public virtual T Create(T entity)
        {
            var entry = Set.Add(entity);
            Context.SaveChanges();
            return entry.Entity;
        }

        public virtual T Update(T entity)
        {
            var entry = Context.Attach(entity);
            entry.State = EntityState.Modified;
            Context.SaveChanges();
            return entry.Entity;
        }

        public virtual void Delete(params int[] ids)
        {
            Set.RemoveRange(Set.Where(x => ids.Contains(x.Id)));
            Context.SaveChanges();
        }

        protected virtual IQueryable<T> Include(IQueryable<T> query) => query;
     }
}