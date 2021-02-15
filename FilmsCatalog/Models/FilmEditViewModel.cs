namespace FilmsCatalog.Models
{
    public class FilmEditViewModel : FilmCreateViewModel
    {
        public int Id { get; set; }
        public string PosterImg { get; set; }
    }
}