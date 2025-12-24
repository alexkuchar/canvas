namespace Canvas.Application.Options;

public class EmailOptions
{
    public string ApiKey { get; set; } = null!;
    public string FromEmail { get; set; } = null!;
    public string FromName { get; set; } = null!;
    public string VerificationEmailTemplateId { get; set; } = null!;
    public string PasswordResetEmailTemplateId { get; set; } = null!;
}

