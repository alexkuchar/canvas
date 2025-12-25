using Canvas.Application.Options;
using Canvas.Application.Repositories;
using Microsoft.Extensions.Options;
using Resend;

namespace Canvas.Infrastructure.Repositories;

public class EmailRepository : IEmailRepository
{

    private readonly IResend _resend;
    private readonly EmailOptions _options;
    private readonly string _signature = "Alex at Canvas";

    public EmailRepository(IResend resend, IOptions<EmailOptions> options)
    {
        _resend = resend;
        _options = options.Value;
    }

    public async Task SendVerificationEmailAsync(string email, string firstName, string verificationLink)
    {
        var msg = new EmailMessage()
        {
            From = _options.From,
            To = email,
            Subject = "Verify your Canvas account",
            HtmlBody = $$"""
                <p>Hello, {{firstName}}!</p>
                <p>Thank you for signing up for Canvas. Please click the link below to verify your account.</p>
                <a href="{{verificationLink}}" target="_blank">Verify your account</a>
                <p>If the link doesn't work, copy and paste this URL into your browser:</p>
                <p>{{verificationLink}}</p>
                <p>If you didn't request this verification, you can safely ignore this email.</p>
                <p>Best wishes,</p>
                <p>{{_signature}}</p>
            """
        };

        await _resend.EmailSendAsync(msg);
    }

    public async Task SendPasswordResetEmailAsync(string email, string firstName, string passwordResetLink)
    {
        var msg = new EmailMessage()
        {
            From = _options.From,
            To = email,
            Subject = "Reset your Canvas password",
            HtmlBody = $$"""
                <p>Hello, {{firstName}}!</p>
                <p>We received a request to reset your Canvas account password. Click the link below to create a new password.</p>
                <a href="{{passwordResetLink}}" target="_blank">Reset your password</a>
                <p>If the link doesn't work, copy and paste this URL into your browser:</p>
                <p>{{passwordResetLink}}</p>
                <p>This link will expire in 15 minutes for security reasons.</p>
                <p>If you didn't request a password reset, you can safely ignore this email. Your password will not be changed.</p>
                <p>Best wishes,</p>
                <p>{{_signature}}</p>
            """
        };

        await _resend.EmailSendAsync(msg);
    }

    public async Task SendWelcomeEmailAsync(string email, string firstName)
    {
        var msg = new EmailMessage()
        {
            From = _options.From,
            To = email,
            Subject = "Welcome to Canvas",
            HtmlBody = $$"""
                <p>Hello, {{firstName}}!</p>
                <p>Welcome to Canvas! Your email has been verified and your account is now active.</p>
                <p>If you have any questions or need help getting started, feel free to reach out.</p>
                <p>Best wishes,</p>
                <p>{{_signature}}</p>
            """
        };

        await _resend.EmailSendAsync(msg);
    }

    public async Task SendPasswordChangedEmailAsync(string email, string firstName, DateTime timestamp)
    {
        var msg = new EmailMessage()
        {
            From = _options.From,
            To = email,
            Subject = "Your Canvas account password was changed",
            HtmlBody = $$"""
                <p>Hello, {{firstName}}!</p>
                <p>Your Canvas account password was successfully changed on {{timestamp:dddd, MMMM dd, yyyy}} at {{timestamp:HH:mm}} UTC.</p>
                <p>If you made this change, no further action is needed.</p>
                <p>If you did NOT make this change, your account may be compromised. Please reset your password immediately.</p>
                <p>Best wishes,</p>
                <p>{{_signature}}</p>
            """
        };

        await _resend.EmailSendAsync(msg);
    }
}