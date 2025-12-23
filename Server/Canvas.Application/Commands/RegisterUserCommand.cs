namespace Canvas.Application.Commands;

public record RegisterUserCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
);