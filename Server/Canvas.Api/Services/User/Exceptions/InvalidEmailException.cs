namespace Canvas.Api.Services.User.Exceptions;

public class InvalidEmailException : Exception
{
    public InvalidEmailException(string email) : base($"Invalid email address '{email}'")
    {
    }
}