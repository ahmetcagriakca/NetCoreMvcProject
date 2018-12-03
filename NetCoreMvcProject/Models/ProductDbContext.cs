using Microsoft.EntityFrameworkCore;

namespace NetCoreMvcProject.Models
{
    public class ProductDbContext : DbContext
    {

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Price);
                entity.Property(x => x.Stock);
                entity.Property(x => x.Image).HasMaxLength(250);
                entity.HasOne(x => x.Brand);
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
            });

        }
    }
}
