using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FilmsCatalog.Models.Db;

namespace FilmsCatalog.Data
{   // Todo move data access context and entities to external project
    public class ApplicationDbContext : IdentityDbContext<User> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Film> Films { get; set; }
        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Film>(e =>
            {
                e.HasOne(x => x.Director).WithMany(x => x.Films);
            });
            builder.Entity<Director>(e =>
            {
                e.HasIndex(x => x.Name);
            });
        }
    }
}
