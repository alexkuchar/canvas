namespace Canvas.Api.Services.Auth.Commands;

public record LoginUserCommand
(
    string Email,
    string Password
);