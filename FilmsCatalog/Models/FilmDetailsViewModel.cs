using System.ComponentModel.DataAnnotations;
using FilmsCatalog.Models.Db;

namespace FilmsCatalog.Models
{
    public class FilmDetailsViewModel : FilmEditViewModel
    {
        public User User { get; set; }
    }
}