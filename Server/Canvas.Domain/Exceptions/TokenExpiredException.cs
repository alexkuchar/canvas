using Canvas.Domain.Errors;

namespace Canvas.Domain.Exceptions;

public class TokenExpiredException : AppException
{
    public TokenExpiredException() : base(ErrorCodes.TokenExpired, "Token expired")
    {
    }
}