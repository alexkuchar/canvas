namespace Canvas.Application.Results;

public record AuthResult(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);