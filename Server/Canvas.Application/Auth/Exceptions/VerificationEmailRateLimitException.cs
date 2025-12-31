using Canvas.Domain.Errors;

namespace Canvas.Application.Auth.Exceptions;

public class VerificationEmailRateLimitException : AppException
{
    public VerificationEmailRateLimitException(int remainingThreshold) : base(ErrorCodes.VerificationEmailRateLimit, $"Please wait {remainingThreshold} seconds before requesting a new verification email.")
    {
    }
}