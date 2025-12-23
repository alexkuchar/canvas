namespace Canvas.Api.Data.DTOs;

public record LoginResponseDto(
    UserDto User,
    TokenPairDto Tokens
);

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);

public record TokenPairDto(
    string AccessToken,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt
);