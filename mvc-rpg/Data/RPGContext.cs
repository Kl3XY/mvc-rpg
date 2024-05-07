using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using mvc_rpg.Models;

namespace mvc_rpg.Data
{
    public class RPGContext : DbContext
    {
        public RPGContext(DbContextOptions<RPGContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<EnemyType> EnemyTypes { get; set; }
        public DbSet<Grave> Graves { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Enemy>().ToTable("Enemy");
            modelBuilder.Entity<EnemyType>().ToTable("EnemyType");
            modelBuilder.Entity<Grave>().ToTable("Grave");

            modelBuilder.Entity<Player>()
            .HasMany(e => e.Items)
            .WithMany(e => e.Players);
        }
    }
}
