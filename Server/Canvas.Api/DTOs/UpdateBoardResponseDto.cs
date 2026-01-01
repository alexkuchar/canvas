namespace Canvas.Api.DTOs;

public record UpdateBoardResponseDto(
    Guid Id,
    string Title,
    string? Description
);