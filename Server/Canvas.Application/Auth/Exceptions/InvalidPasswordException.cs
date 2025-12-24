using Canvas.Domain.Errors;

namespace Canvas.Application.Auth.Exceptions;

public class InvalidPasswordException : AppException
{
    public InvalidPasswordException() : base(ErrorCodes.InvalidPassword, "Invalid credentials")
    {
    }
}