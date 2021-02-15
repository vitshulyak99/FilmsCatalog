using System.Collections.Generic;

namespace FilmsCatalog.Models.Db
{
    public class Director : BaseEntity
    {
        public string Name { get; set; }

        public List<Film> Films { get; set; }
    }
}