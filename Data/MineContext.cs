using exomine.Data.Configurations;
using exomine.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace exomine.Data
{
    public class MineContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.Entity<UserGame>().HasKey(ug => new { ug.UserId, ug.GameId });
        }
    }
}