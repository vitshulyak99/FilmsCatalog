using System.Collections.Generic;
using FilmsCatalog.Models.Db;

namespace FilmsCatalog.Services.Interfaces
{
    public interface IDirectorService : ICrudService<Director>
    {
        public IEnumerable<Director> GetMatch(string text);
    }
}