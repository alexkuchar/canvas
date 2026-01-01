namespace Canvas.Api.DTOs;

public record CreateBoardResponseDto(
    Guid Id,
    string Title,
    string? Description
);