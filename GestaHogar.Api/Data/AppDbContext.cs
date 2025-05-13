using GestaHogar.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaHogar.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithMany(p => p.Users)
                .UsingEntity<UserProduct>();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Users)
                .WithMany(u => u.Products)
                .UsingEntity<UserProduct>();
        }
    }
}
