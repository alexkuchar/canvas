using Canvas.Api.Data;
using Canvas.Api.Extensions.Authentication;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Canvas.Api.Middleware;
using Canvas.Application.Security;
using Canvas.Application.Services.Auth;
using Canvas.Application.Services.User;
using Canvas.Application.Repositories;
using Canvas.Infrastructure.Security;
using Canvas.Infrastructure.Repositories;
using Canvas.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthenticationAndAuthorization();
app.MapControllers();

app.Run();