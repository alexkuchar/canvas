using Canvas.Domain.Errors;

namespace Canvas.Application.Auth.Exceptions;

public class SessionExpiredException : AppException
{
    public SessionExpiredException() : base(ErrorCodes.SessionExpired, "Session expired")
    {
    }
}