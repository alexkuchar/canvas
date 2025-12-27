namespace Canvas.Application.Auth.Exceptions;
using Canvas.Domain.Errors;
public class UserNotVerifiedException : AppException
{
    public UserNotVerifiedException() : base(ErrorCodes.UserNotVerified, "User not verified")
    {
    }
}