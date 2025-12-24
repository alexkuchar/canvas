namespace Canvas.Api.DTOs;

public record RefreshResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);