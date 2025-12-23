using Canvas.Domain.Errors;

namespace Canvas.Api.Services.Auth.Exceptions;

public class SessionNotFoundException : AppException
{
    public SessionNotFoundException(string refreshToken) : base(ErrorCodes.SessionNotFound, $"Session with refresh token {refreshToken} not found")
    {
    }
}