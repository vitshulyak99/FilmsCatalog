using System.Collections.Generic;
using System.Linq;
using FilmsCatalog.Data;
using FilmsCatalog.Models.Db;
using FilmsCatalog.Services.Interfaces;

namespace FilmsCatalog.Services
{
    public class DirectorService : BaseCrudService<Director> , IDirectorService
    {
        public DirectorService(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Director> GetMatch(string text) =>
            Set.Where(x => x.Name.StartsWith(text)).ToList();
    }
}