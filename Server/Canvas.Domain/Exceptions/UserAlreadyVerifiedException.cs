namespace Canvas.Domain.Exceptions;

public class UserAlreadyVerifiedException : Exception
{
    public UserAlreadyVerifiedException() : base("User already verified")
    {
    }
}