using GestaHogar.Models;
using Microsoft.AspNetCore.Identity;

namespace GestaHogar.Api.Data
{
    public class Seeder(AppDbContext appDbContext)
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        internal void Seed()
        {
            this.SeedData();

            _appDbContext.SaveChanges();
        }

        private void SeedData()
        {
            _appDbContext.Products.Add(
                new Product()
                {
                    Name = "Arroz",
                    Category = "Comida",
                    Amount = 1000,
                    Unit = Util.EUnit.Gram,
                }
            );

            _appDbContext.Products.Add(
                new Product()
                {
                    Name = "Garvanzos",
                    Category = "Comida",
                    Amount = 1,
                    Unit = Util.EUnit.Jar,
                }
            );

            _appDbContext.Products.Add(
                new Product()
                {
                    Name = "Jabón",
                    Category = "Higiene",
                    Amount = 2,
                    Unit = Util.EUnit.Liter,
                }
            );

            User userA = default!;
            var hasher = new PasswordHasher<User>();

            _appDbContext.Users.Add(
                userA = new User()
                {
                    Email = "a@admin.com",
                    UserName = "a",
                    PasswordHash = hasher.HashPassword(userA, "c0ntras3nha_"),
                }
            );

            User userB = default!;

            _appDbContext.Users.Add(
                userB = new User()
                {
                    Email = "b@test.com",
                    UserName = "b",
                    PasswordHash = hasher.HashPassword(userB, "c0ntras3nha!"),
                }
            );

            _appDbContext.UserProducts.Add(
                new UserProduct()
                {
                    ProductId = 1,
                    UserId = userA.Id,
                    CurrentStock = 2000,
                    NormalStock = 3000,
                    DailyUse = 100,
                }
            );

            _appDbContext.UserProducts.Add(
                new UserProduct()
                {
                    ProductId = 1,
                    UserId = userB.Id,
                    CurrentStock = 2500,
                    NormalStock = 4000,
                    DailyUse = 250,
                }
            );

            _appDbContext.UserProducts.Add(
                new UserProduct()
                {
                    ProductId = 2,
                    UserId = userA.Id,
                    CurrentStock = 3,
                    NormalStock = 5,
                    DailyUse = 0.25f,
                }
            );
        }

        //La documentación dice que es una buena practica, pero puede que la esté aplicando mal
        internal async Task SeedAsync(CancellationToken cancellationToken)
        {
            this.SeedData();

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
