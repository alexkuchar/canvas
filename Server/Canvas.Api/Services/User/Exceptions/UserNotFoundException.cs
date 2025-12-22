using Canvas.Api.Errors;

namespace Canvas.Api.Services.User.Exceptions;

public class UserNotFoundException : AppException
{
    public UserNotFoundException(Guid id) : base(ErrorCodes.UserNotFound, $"User with id {id} not found")
    {
    }

    public UserNotFoundException(string email) : base(ErrorCodes.UserNotFound, $"User with email {email} not found")
    {
    }
}