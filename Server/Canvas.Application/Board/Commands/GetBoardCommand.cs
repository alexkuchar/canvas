namespace Canvas.Application.Board.Commands;

public record GetBoardCommand(
    Guid userId,
    Guid boardId
);
