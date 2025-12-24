using Canvas.Domain.Errors;

namespace Canvas.Application.Auth.Exceptions;

public class VerificationTokenUsedException : AppException
{
    public VerificationTokenUsedException() : base(ErrorCodes.VerificationTokenUsed,
        "This verification token has already been used")
    {
    }
}