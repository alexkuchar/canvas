using Canvas.Domain.Errors;

namespace Canvas.Application.Exceptions;

public class SessionRevokedException : AppException
{
    public SessionRevokedException() : base(ErrorCodes.SessionRevoked, "Session revoked")
    {
    }
}