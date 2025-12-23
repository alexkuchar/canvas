namespace Canvas.Domain.Errors;

public class AppException : Exception
{
    public string Code { get; }
    public override string Message { get; }

    public AppException(string code, string message)
    {
        Code = code;
        Message = message;
    }
}