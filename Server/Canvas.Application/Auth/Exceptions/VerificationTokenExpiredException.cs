using Canvas.Domain.Errors;

namespace Canvas.Application.Auth.Exceptions;

public class VerificationTokenExpiredException : AppException
{
    public VerificationTokenExpiredException() : base(ErrorCodes.VerificationTokenExpired, "This verification token is expired.") {}
}