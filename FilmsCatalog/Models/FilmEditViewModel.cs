using System.ComponentModel.DataAnnotations;

namespace FilmsCatalog.Models
{
    public class FilmEditViewModel : FilmCreateViewModel
    {
        [Required]
        public int Id { get; set; }
        public string PosterImg { get; set; }
    }
}