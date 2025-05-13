using GestaHogar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GestaHogar.Api.Data
{
    public class UserDbContext(DbContextOptions options) : IdentityDbContext<User>(options)
    {
    }

}
