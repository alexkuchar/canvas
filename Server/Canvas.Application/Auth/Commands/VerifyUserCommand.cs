namespace Canvas.Application.Auth.Commands;

public record VerifyUserCommand(
    string VerificationToken
);