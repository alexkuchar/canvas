namespace Canvas.Api.Data.DTOs;

public record RegisterRequestDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);