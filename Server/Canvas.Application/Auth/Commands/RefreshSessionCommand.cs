namespace Canvas.Application.Auth.Commands;

public record RefreshSessionCommand(
    string RefreshToken
);