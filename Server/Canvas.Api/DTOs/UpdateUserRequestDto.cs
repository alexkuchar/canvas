namespace Canvas.Api.DTOs;

public record UpdateUserRequestDto(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password
);