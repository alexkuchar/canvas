using Canvas.Domain.Errors;

namespace Canvas.Application.Board.Exceptions;

public class BoardNotFoundException : AppException
{
    public BoardNotFoundException(Guid boardId) : base(ErrorCodes.BoardNotFound, $"Board with id {boardId} not found")
    {
    }
}