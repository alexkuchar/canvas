namespace Canvas.Application.Auth.Commands;

public record ResetPasswordCommand(
    string Token,
    string NewPassword
);