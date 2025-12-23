using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Canvas.Infrastructure.Extensions.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var jwt = configuration.GetSection("Jwt").Get<JwtOptions>() ?? throw new InvalidOperationException("JWT configuration is not configured");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = key,

                ValidIssuer = jwt.Issuer,
                ValidAudience = jwt.Audience,

                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.FromSeconds(30),
            };

        });
        services.AddAuthorization();
        return services;
    }

    public static WebApplication UseAuthenticationAndAuthorization(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}

