namespace Canvas.Api.DTOs;

public record LoginRequestDto(
    string Email,
    string Password
);