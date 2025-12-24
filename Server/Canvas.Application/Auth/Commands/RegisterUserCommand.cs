namespace Canvas.Application.Auth.Commands;

public record RegisterUserCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
);