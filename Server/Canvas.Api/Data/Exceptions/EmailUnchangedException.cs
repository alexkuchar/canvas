using Canvas.Api.Errors;

namespace Canvas.Api.Data.Exceptions;

public class EmailUnchangedException : AppException
{
    public EmailUnchangedException() : base(ErrorCodes.EmailUnchanged, "Email is unchanged")
    {
    }
}