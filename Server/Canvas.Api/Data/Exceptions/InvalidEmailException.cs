using Canvas.Api.Errors;

namespace Canvas.Api.Data.Exceptions;

public class InvalidEmailException : AppException
{
    public InvalidEmailException(string email) : base(ErrorCodes.InvalidEmail, $"Invalid email address '{email}'")
    {
    }
}