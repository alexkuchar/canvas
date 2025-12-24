using Canvas.Application.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Canvas.Infrastructure.Extensions;

public static class EmailExtensions
{
    public static IServiceCollection AddEmailConfiguration(this IServiceCollection services)
    {
        services.Configure<EmailOptions>(options =>
        {
            options.ApiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ??
                throw new InvalidOperationException("SENDGRID_API_KEY is not set");
            options.FromEmail = Environment.GetEnvironmentVariable("FROM_EMAIL") ??
                throw new InvalidOperationException("FROM_EMAIL is not set");
            options.FromName = Environment.GetEnvironmentVariable("FROM_NAME") ??
                throw new InvalidOperationException("FROM_NAME is not set");
            options.VerificationEmailTemplateId = Environment.GetEnvironmentVariable("VERIFICATION_EMAIL_TEMPLATE_ID") ??
                throw new InvalidOperationException("VERIFICATION_EMAIL_TEMPLATE_ID is not set");
            options.PasswordResetEmailTemplateId = Environment.GetEnvironmentVariable("PASSWORD_RESET_EMAIL_TEMPLATE_ID") ??
                throw new InvalidOperationException("PASSWORD_RESET_EMAIL_TEMPLATE_ID is not set");
        });

        return services;
    }
}

