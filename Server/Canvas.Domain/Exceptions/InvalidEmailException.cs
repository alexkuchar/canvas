using Canvas.Domain.Errors;

namespace Canvas.Domain.Exceptions;

public class InvalidEmailException : AppException
{
    public InvalidEmailException(string email) : base(ErrorCodes.InvalidEmail, $"Invalid email address '{email}'")
    {
    }
}