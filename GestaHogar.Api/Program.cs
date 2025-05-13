using GestaHogar.Api.Data;
using GestaHogar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<UserDbContext>(opt =>
    opt.UseMySQL(builder.Configuration.GetConnectionString("MariaDbConnection")!)
);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySQL(builder.Configuration.GetConnectionString("MariaDbConnection")!)
);

builder
    .Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

//map routes here

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
