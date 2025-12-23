namespace Canvas.Api.Data.DTOs;

public record RefreshResponseDto(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);