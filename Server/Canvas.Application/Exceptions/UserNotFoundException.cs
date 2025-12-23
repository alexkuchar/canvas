using Canvas.Domain.Errors;

namespace Canvas.Application.Exceptions;

public class UserNotFoundException : AppException
{
    public UserNotFoundException(Guid id) : base(ErrorCodes.UserNotFound, $"User with id {id} not found")
    {
    }

    public UserNotFoundException(string email) : base(ErrorCodes.UserNotFound, $"User with email {email} not found")
    {
    }
}