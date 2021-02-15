using System.Collections.Generic;
using System.Linq;
using FilmsCatalog.Models.Db;

namespace FilmsCatalog.Services.Interfaces
{
    public interface ICrudService<T> where T : BaseEntity
    {
        int Count { get; }
        IQueryable<T> Query();
        IEnumerable<T> GetAll();
        T GetById(int id);
        T Create(T entity);
        T Update(T entity);
        void Delete(params int[] ids);
    }
}