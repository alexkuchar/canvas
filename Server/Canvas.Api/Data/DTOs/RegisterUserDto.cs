namespace Canvas.Api.Data.DTOs;

public record RegisterUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password
);