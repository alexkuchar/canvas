using Microsoft.OpenApi;

namespace Canvas.Api.Extensions;

public static class AddSwaggerDocsExtension
{
    public static IServiceCollection AddSwaggerDocs(this IServiceCollection services)
    {
        services.AddOpenApi();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Canvas API", Version = "v1" });
        });

        return services;
    }

    public static WebApplication UseSwaggerDocs(this WebApplication app)
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "Canvas API v1");
        });

        return app;
    }
}