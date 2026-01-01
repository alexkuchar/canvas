namespace Canvas.Application.Board.Exceptions;
using Canvas.Domain.Errors;

public class BoardNotOwnedByUserException : AppException
{
    public BoardNotOwnedByUserException(Guid boardId) : base(ErrorCodes.BoardNotOwnedByUser, $"Board with id {boardId} is not owned by user")
    {
    }
}