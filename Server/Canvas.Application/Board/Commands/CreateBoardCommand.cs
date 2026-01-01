namespace Canvas.Application.Board.Commands;

public record CreateBoardCommand(
    Guid userId,
    string title,
    string? description
);