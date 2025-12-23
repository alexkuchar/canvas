using Canvas.Domain.Errors;

namespace Canvas.Application.Exceptions;

public class SessionAlreadyRevokedException : AppException
{
    public SessionAlreadyRevokedException() : base(ErrorCodes.SessionAlreadyRevoked, "Session already revoked")
    {
    }
}