namespace Canvas.Api.Services.Auth.Commands;

public record RefreshSessionCommand(
    string RefreshToken
);