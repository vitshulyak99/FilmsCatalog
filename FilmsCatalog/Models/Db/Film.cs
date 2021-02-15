namespace FilmsCatalog.Models.Db
{
    public class Film : BaseEntity
    {   
        public string Name { get; set; }

        public string Description { get; set; }

        public int Year { get; set; }

        public string Poster { get; set; }

        public Director Director { get; set; }

        public User AddedAt { get; set; }
    }
}