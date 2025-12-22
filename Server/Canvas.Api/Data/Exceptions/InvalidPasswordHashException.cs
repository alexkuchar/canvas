using Canvas.Api.Errors;

namespace Canvas.Api.Data.Exceptions;

public class InvalidPasswordHashException : AppException
{
    public InvalidPasswordHashException() : base(ErrorCodes.InvalidPasswordHash, "Invalid password hash")
    {
    }
}