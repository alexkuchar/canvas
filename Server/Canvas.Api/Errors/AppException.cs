namespace Canvas.Api.Errors;

public class AppException : Exception
{
    public string Code { get; }
    public string Message { get; }

    public AppException(string code, string message)
    {
        Code = code;
        Message = message;
    }
}