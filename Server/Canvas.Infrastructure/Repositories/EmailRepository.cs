using Canvas.Application.Options;
using Canvas.Application.Repositories;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Canvas.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository
{

    private readonly EmailOptions _options;
    private readonly SendGridClient _sendGridClient;

    public EmailRepository(IOptions<EmailOptions> options)
    {
        _options = options.Value;
        _sendGridClient = new SendGridClient(_options.ApiKey);
    }

    public async Task SendVerificationEmailAsync(string email, string firstName, string verificationLink)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_options.FromEmail, _options.FromName),
            TemplateId = _options.VerificationEmailTemplateId,
        };

        msg.AddTo(new EmailAddress(email));

        msg.SetTemplateData(new
        {
            firstName = firstName,
            verificationLink = verificationLink,
            signature = _options.FromName
        });

        var response = await _sendGridClient.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to send verification email: {response.StatusCode}");
        }
    }

    public async Task SendPasswordResetEmailAsync(string email, string firstName, string passwordResetLink)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_options.FromEmail, _options.FromName),
            TemplateId = _options.PasswordResetEmailTemplateId,
        };

        msg.AddTo(new EmailAddress(email));

        msg.SetTemplateData(new
        {
            firstName = firstName,
            passwordResetLink = passwordResetLink,
            signature = _options.FromName
        });

        var response = await _sendGridClient.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to send password reset email: {response.StatusCode}");
        }
    }
}