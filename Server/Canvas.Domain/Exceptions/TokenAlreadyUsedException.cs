using Canvas.Domain.Errors;

namespace Canvas.Domain.Exceptions;

public class TokenAlreadyUsedException : AppException
{
    public TokenAlreadyUsedException() : base(ErrorCodes.TokenAlreadyUsed, "Token already used")
    {
    }
}