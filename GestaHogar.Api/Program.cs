#define DEBUG

using GestaHogar.Api.Data;
using GestaHogar.Client;
using GestaHogar.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(opt =>

#if DEBUG
    opt.UseInMemoryDatabase("TestGestaHogar")
#else
    opt.UseMySQL(builder.Configuration.GetConnectionString("MariaDbConnection")!)
#endif
);

builder
    .Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication().
    AddJwtBearer(GHHttpClient.AUTH_SCHEME, opt =>
    {
        opt.Audience = builder.Configuration["Jwt:Audience"]!;
        opt.Authority = builder.Configuration["Jwt:Authority"]!;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

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
    .RequireAuthorization();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
