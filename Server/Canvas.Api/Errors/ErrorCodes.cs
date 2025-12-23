namespace Canvas.Api.Errors;

public static class ErrorCodes
{
    // User
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string EmailAlreadyInUse = "EMAIL_ALREADY_IN_USE";
    public const string EmailUnchanged = "EMAIL_UNCHANGED";
    public const string InvalidEmail = "INVALID_EMAIL";
    public const string InvalidPasswordHash = "INVALID_PASSWORD_HASH";
    public const string InvalidPassword = "INVALID_PASSWORD";

    // General
    public const string UnexpectedError = "UNEXPECTED_ERROR";
}