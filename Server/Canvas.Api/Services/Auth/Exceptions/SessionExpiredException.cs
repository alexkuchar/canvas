using Canvas.Domain.Errors;

namespace Canvas.Api.Services.Auth.Exceptions;

public class SessionExpiredException : AppException
{
    public SessionExpiredException() : base(ErrorCodes.SessionExpired, "Session expired")
    {
    }
}