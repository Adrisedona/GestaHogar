using GestaHogar.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaHogar.Api.Data
{
    public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
    }
    
}
