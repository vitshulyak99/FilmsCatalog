using System.Collections.Generic;

namespace FilmsCatalog.Models.Db
{

    public class BaseEntity
    {
        public string Id { get; set; }
    }

    public class Film : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string Poster { get; set; }
        public Director Director { get; set; }
        public User AddedAt { get; set; }
    }

    public class Director : BaseEntity
    {
        public string Name { get; set; }
        public List<Film> Films { get; set; }
    }
}