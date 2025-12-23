using Canvas.Domain.Errors;

namespace Canvas.Domain.Exceptions;

public class EmailUnchangedException : AppException
{
    public EmailUnchangedException() : base(ErrorCodes.EmailUnchanged, "Email is unchanged")
    {
    }
}