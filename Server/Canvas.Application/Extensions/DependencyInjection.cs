using Canvas.Application.Auth.Handlers;
using Canvas.Application.Board.Handlers;
using Canvas.Application.Options;
using Canvas.Application.User.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Canvas.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

        // Auth
        services.AddScoped<RegisterUserHandler>();
        services.AddScoped<LoginUserHandler>();
        services.AddScoped<RefreshSessionHandler>();
        services.AddScoped<VerifyUserHandler>();
        services.AddScoped<ForgotPasswordHandler>();
        services.AddScoped<ResetPasswordHandler>();
        services.AddScoped<ResendVerificationEmailHandler>();

        // User
        services.AddScoped<UpdateUserHandler>();

        // Board
        services.AddScoped<CreateBoardHandler>();
        services.AddScoped<DeleteBoardHandler>();
        services.AddScoped<GetBoardHandler>();
        services.AddScoped<UpdateBoardHandler>();
        services.AddScoped<GetBoardsHandler>();

        return services;
    }
}

