using Canvas.Api.Errors;

namespace Canvas.Api.Services.Auth.Exceptions;

public class InvalidPasswordException : AppException
{
    public InvalidPasswordException() : base(ErrorCodes.InvalidPassword, "Invalid credentials")
    {
    }
}