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
            // Le decimos: "En C# es long, pero en la DB léelo y escríbelo como int"
            modelBuilder.Entity<Beer>()
                .Property(b => b.BeerID)
                .HasConversion<int>();

            modelBuilder.Entity<Beer>()
                .Property(b => b.BrandID)
                .HasConversion<int>();

            modelBuilder.Entity<Brand>()
                .Property(b => b.Id)
                .HasConversion<int>();
        }


    }
}
