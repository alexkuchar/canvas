namespace Canvas.Application.Repositories;

public interface IEmailRepository
{
    Task SendVerificationEmailAsync(string email, string firstName, string verificationToken);
    Task SendWelcomeEmailAsync(string email, string firstName);
    Task SendPasswordResetEmailAsync(string email, string firstName, string passwordResetToken);
    Task SendPasswordChangedEmailAsync(string email, string firstName, DateTime timestamp);
}