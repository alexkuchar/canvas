namespace Canvas.Application.Repositories;

public interface IEmailRepository
{
    Task SendVerificationEmailAsync(string email, string firstName, string verificationLink);
    Task SendWelcomeEmailAsync(string email, string firstName);
    Task SendPasswordResetEmailAsync(string email, string firstName, string passwordResetLink);
    Task SendPasswordChangedEmailAsync(string email, string firstName, DateTime timestamp);
}