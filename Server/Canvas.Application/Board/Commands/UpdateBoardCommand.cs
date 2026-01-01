namespace Canvas.Application.Board.Commands;

public record UpdateBoardCommand(
    Guid userId,
    Guid boardId,
    string title,
    string? description
);