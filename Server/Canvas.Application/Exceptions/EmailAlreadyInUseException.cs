using Canvas.Domain.Errors;

namespace Canvas.Application.Exceptions;

public class EmailAlreadyInUseException : AppException
{
    public EmailAlreadyInUseException(string email) : base(ErrorCodes.EmailAlreadyInUse, $"Email '{email}' is already in use")
    {
    }
}