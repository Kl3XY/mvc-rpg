using Microsoft.EntityFrameworkCore;
using mvc_rpg.Models;

namespace mvc_rpg.Data
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions<RPGContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Player> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Item>().ToTable("Item");
        }
    }
}
