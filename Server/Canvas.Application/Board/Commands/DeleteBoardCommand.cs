namespace Canvas.Application.Board.Commands;

public record DeleteBoardCommand(
    Guid userId,
    Guid boardId
);