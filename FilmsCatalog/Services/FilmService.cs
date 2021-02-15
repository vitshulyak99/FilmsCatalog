using System.Linq;
using FilmsCatalog.Data;
using FilmsCatalog.Models.Db;
using FilmsCatalog.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FilmsCatalog.Services
{
    public class FilmService : BaseCrudService<Film> , IFilmService
    {
        public FilmService(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<Film> Include(IQueryable<Film> query) =>
            query.Include(x=>x.Director);

        public override Film GetById(int id) =>
            Include(Set.Where(x => x.Id.Equals(id))).Include(x => x.AddedAt).FirstOrDefault();

        public override Film Create(Film entity)
        {
            if (entity?.AddedAt?.Id is not null)
            {
                Context.Attach(entity.AddedAt);
            }

            if (entity?.Director is null) return base.Create(entity);

            CheckDirector(entity);

            return base.Create(entity);
        }

        public override Film Update(Film entity)
        {
            CheckDirector(entity);
            return base.Update(entity);
        }

        public bool CheckOwner(int id,string userId) => Set.Any(x => x.Id.Equals(id) && x.AddedAt.Id.Equals(userId));

        private void CheckDirector(Film entity)
        {
            var directorSet = Context.Set<Director>();
            var director = directorSet.FirstOrDefault(x => x.Name.ToLower().Equals(entity.Director.Name));

            if (director is not null)
            {
                entity.Director = director;
            }
        }
    }
}