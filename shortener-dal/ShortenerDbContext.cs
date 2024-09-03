using Microsoft.EntityFrameworkCore;
using url_shortener_server.shortener_dal.Entities;

namespace url_shortener_server.shortener_dal
{
    public class ShortenerDbContext : DbContext
    {

        public ShortenerDbContext(DbContextOptions<ShortenerDbContext> options)
            :base(options)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!File.Exists("temp.db"))
            {
                File.Create("temp.db").Dispose();
            }

            optionsBuilder.UseSqlite("Data source=temp.db");
        }

        public virtual DbSet<Link> Links { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
    }
}
