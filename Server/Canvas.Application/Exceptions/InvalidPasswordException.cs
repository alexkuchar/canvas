using Canvas.Domain.Errors;

namespace Canvas.Application.Exceptions;

public class InvalidPasswordException : AppException
{
    public InvalidPasswordException() : base(ErrorCodes.InvalidPassword, "Invalid credentials")
    {
    }
}