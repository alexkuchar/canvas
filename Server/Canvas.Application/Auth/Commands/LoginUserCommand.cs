namespace Canvas.Application.Auth.Commands;

public record LoginUserCommand
(
    string Email,
    string Password
);