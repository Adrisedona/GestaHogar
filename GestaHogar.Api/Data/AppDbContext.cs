using GestaHogar.Models;
using GestaHogar.Util;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GestaHogar.Api.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        //optionsBuilder.UseInMemoryDatabase("TestGestaHogar");
        //        optionsBuilder.UseMySQL(optionsBuilder.);
        //    }

        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var nonNegativeFloatConverter = new ValueConverter<UFloat, float>(
                v => v.Value,
                v => new UFloat(v)
            );
            modelBuilder.Entity<User>()
                .HasMany(u => u.Products)
                .WithMany(p => p.Users)
                .UsingEntity<UserProduct>();

            modelBuilder.Entity<UserProduct>()
                .Property(up => up.CurrentStock)
                .HasConversion(nonNegativeFloatConverter);

            modelBuilder.Entity<UserProduct>()
                .Property(up => up.DailyUse)
                .HasConversion(nonNegativeFloatConverter);

            modelBuilder.Entity<UserProduct>()
                .Property(up => up.NormalStock)
                .HasConversion(nonNegativeFloatConverter);

            modelBuilder.Entity<UserProduct>()
                .HasKey(up => new { up.ProductId, up.UserId });

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Users)
                .WithMany(u => u.Products)
                .UsingEntity<UserProduct>();

            modelBuilder.Entity<Product>()
                .Property(p => p.Amount)
                .HasConversion(nonNegativeFloatConverter);

            modelBuilder.Entity<Product>()
                .Property(p => p.Unit)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }

}
