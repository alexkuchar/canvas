using Canvas.Api.Errors;

namespace Canvas.Api.Services.Auth.Exceptions;

public class SessionAlreadyRevokedException : AppException
{
    public SessionAlreadyRevokedException() : base(ErrorCodes.SessionAlreadyRevoked, "Session already revoked")
    {
    }
}