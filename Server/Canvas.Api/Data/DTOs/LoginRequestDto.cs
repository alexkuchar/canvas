namespace Canvas.Api.Data.DTOs;

public record LoginRequestDto(
    string Email,
    string Password
);