namespace Canvas.Application.User.Commands;

public record UpdateUserCommand(
    Guid Id,
    string? FirstName,
    string? LastName,
    string? Email,
    string? Password
);