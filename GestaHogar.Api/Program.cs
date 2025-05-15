using GestaHogar.Api.Data;
using GestaHogar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("TestGestaHogar")
//opt.UseMySQL(builder.Configuration.GetConnectionString("MariaDbConnection")!)
);

builder
    .Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

//map routes here
app.MapIdentityApi<User>();
app.MapPost(
        "/logout",
        async (SignInManager<User> signInManager, [FromBody] object empty) =>
        {
            if (empty != null)
            {
                await signInManager.SignOutAsync();
                return Results.Ok();
            }
            return Results.Unauthorized();
        }
    )
    .WithOpenApi()
    .RequireAuthorization();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
