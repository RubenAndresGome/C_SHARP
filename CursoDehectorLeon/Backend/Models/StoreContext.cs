using Microsoft.EntityFrameworkCore;


namespace Backend.Models
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {
        }

        public DbSet<Beer> Beers { get; set; }
        public DbSet<Brand> Brands { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esto "mapea" el bigint de SQL al int de C# automáticamente
            modelBuilder.Entity<Beer>()
                .Property(b => b.BeerID)
                .HasConversion<long>();

            modelBuilder.Entity<Beer>()
                .Property(b => b.BrandID)
                .HasConversion<long>();

            modelBuilder.Entity<Brand>()
                .Property(b => b.Id)
                .HasConversion<long>();
        }

        ///////

    }
}
