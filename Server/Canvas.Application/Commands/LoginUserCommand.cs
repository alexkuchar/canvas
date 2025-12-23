namespace Canvas.Application.Commands;

public record LoginUserCommand
(
    string Email,
    string Password
);