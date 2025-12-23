using Canvas.Domain.Errors;

namespace Canvas.Api.Services.Auth.Exceptions;

public class SessionRevokedException : AppException
{
    public SessionRevokedException() : base(ErrorCodes.SessionRevoked, "Session revoked")
    {
    }
}