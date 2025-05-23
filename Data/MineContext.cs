using exomine.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace exomine.Data
{
    public class MineContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}