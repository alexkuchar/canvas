using Canvas.Domain.Errors;

namespace Canvas.Application.Auth.Exceptions;

public class VerificationTokenNotFoundException : AppException
{
    public VerificationTokenNotFoundException() : base(ErrorCodes.VerificationTokenNotFound, "Verification token not found")
    {
    }
}