using Canvas.Domain.Errors;

namespace Canvas.Domain.Exceptions;

public class InvalidBoardTitleException : AppException
{
    public InvalidBoardTitleException(string title) : base(ErrorCodes.InvalidBoardTitle, $"Invalid board title '{title}'")
    {
    }
}