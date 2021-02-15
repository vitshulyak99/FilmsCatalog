using FilmsCatalog.Models.Db;

namespace FilmsCatalog.Services.Interfaces
{
    public interface IFilmService : ICrudService<Film>
    {
        bool CheckOwner(int id,string userId);
    }
}