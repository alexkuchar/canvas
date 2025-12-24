namespace Canvas.Application.Results;

public record SessionRefreshResult(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);