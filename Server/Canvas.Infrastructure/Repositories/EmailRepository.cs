using Canvas.Application.Repositories;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.VisualBasic;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Canvas.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository
{

    private readonly string _apiKey;
    private readonly string _fromEmail;
    private readonly string _fromName;
    private readonly string _verificationEmailTemplateId;

    private readonly SendGridClient _sendGridClient;

    public EmailRepository()
    {
        _apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY") ??
        throw new InvalidOperationException("SENDGRID_API_KEY is not set");

        _fromEmail = Environment.GetEnvironmentVariable("FROM_EMAIL") ??
        throw new InvalidOperationException("FROM_EMAIL is not set");

        _fromName = Environment.GetEnvironmentVariable("FROM_NAME") ??
        throw new InvalidOperationException("FROM_NAME is not set");

        _verificationEmailTemplateId = Environment.GetEnvironmentVariable("VERIFICATION_EMAIL_TEMPLATE_ID") ??
        throw new InvalidOperationException("VERIFICATION_EMAIL_TEMPLATE_ID is not set");

        _sendGridClient = new SendGridClient(_apiKey);
    }

    public async Task SendVerificationEmailAsync(string email, string firstName, string verificationLink)
    {
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(_fromEmail, _fromName),
            TemplateId = _verificationEmailTemplateId,
        };

        msg.AddTo(new EmailAddress(email));

        msg.SetTemplateData(new
        {
            firstName = firstName,
            verificationLink = verificationLink,
            signature = _fromName
        });

        var response = await _sendGridClient.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Failed to send verification email: {response.StatusCode}");
        }

    }
}