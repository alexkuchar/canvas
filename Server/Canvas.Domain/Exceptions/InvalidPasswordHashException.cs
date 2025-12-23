using Canvas.Domain.Errors;

namespace Canvas.Domain.Exceptions;

public class InvalidPasswordHashException : AppException
{
    public InvalidPasswordHashException() : base(ErrorCodes.InvalidPasswordHash, "Invalid password hash")
    {
    }
}