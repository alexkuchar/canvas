using Canvas.Domain.Errors;

namespace Canvas.Application.Auth.Exceptions;

public class VerificationTokenTypeMismatchException : AppException
{
    public VerificationTokenTypeMismatchException() : base(ErrorCodes.VerificationTokenTypeMismatch, "This verification token type is not valid for this operation")
    {
    }
}