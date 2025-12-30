using DotNetEnv;
using Scalar.AspNetCore;
using Microsoft.Extensions.Configuration;

namespace Canvas.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddOpenApi();
        services.AddCors(configuration);

        return services;
    }

    private static IServiceCollection AddCors(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedOrigins = configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowFrontend", policy =>
            {
                policy.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }

    public static WebApplication UseApi(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            DotNetEnv.Env.TraversePath().Load();
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseCors("AllowFrontend");
        app.UseHttpsRedirection();
        app.MapControllers();

        return app;
    }
}

