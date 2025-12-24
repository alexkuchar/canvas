namespace Canvas.Api.DTOs;

public record RegisterRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);