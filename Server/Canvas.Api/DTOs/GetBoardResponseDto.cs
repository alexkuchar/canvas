namespace Canvas.Api.DTOs;

public record GetBoardResponseDto(
    Guid Id,
    string Title,
    string? Description
);