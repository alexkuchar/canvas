namespace Canvas.Api.DTOs;

public record ResetPasswordRequestDto(
    string Token,
    string NewPassword
);