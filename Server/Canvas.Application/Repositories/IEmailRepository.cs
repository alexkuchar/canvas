namespace Canvas.Application.Repositories;

public interface IEmailRepository
{
    Task SendVerificationEmailAsync(string email, string firstName, string verificationLink);
}