using Canvas.Application.Options;
using Canvas.Application.Repositories;
using Canvas.Application.Security;
using Canvas.Infrastructure.Persistence;
using Canvas.Infrastructure.Repositories;
using Canvas.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Resend;

namespace Canvas.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        // Email
        services.Configure<EmailOptions>(o =>
        {
            o.ApiKey = Environment.GetEnvironmentVariable("RESEND_API_KEY") ?? throw new InvalidOperationException("RESEND_API_KEY is not set");
            o.From = Environment.GetEnvironmentVariable("RESEND_FROM") ?? throw new InvalidOperationException("RESEND_FROM is not set");
        });

        // Resend
        services.AddHttpClient<ResendClient>();
        services.Configure<ResendClientOptions>(o =>
        {
            o.ApiToken = Environment.GetEnvironmentVariable("RESEND_API_KEY") ?? throw new InvalidOperationException("RESEND_API_KEY is not set");
        });
        services.AddTransient<IResend, ResendClient>();

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<IVerificationTokenRepository, VerificationTokenRepository>();

        services.AddScoped<IEmailRepository, EmailRepository>();

        // Services
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenService, TokenService>();

        return services;
    }
}

