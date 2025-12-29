namespace Canvas.Api.DTOs;

public record UpdateUserResponseDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);