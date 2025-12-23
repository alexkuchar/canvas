namespace Canvas.Api.Services.Auth.Results;

public record SessionRefreshResult(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);