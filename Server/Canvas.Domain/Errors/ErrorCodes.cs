namespace Canvas.Domain.Errors;

public static class ErrorCodes
{
    // User
    public const string UserNotFound = "USER_NOT_FOUND";
    public const string EmailAlreadyInUse = "EMAIL_ALREADY_IN_USE";
    public const string EmailUnchanged = "EMAIL_UNCHANGED";
    public const string InvalidEmail = "INVALID_EMAIL";
    public const string InvalidPasswordHash = "INVALID_PASSWORD_HASH";
    public const string InvalidPassword = "INVALID_PASSWORD";
    public const string UserAlreadyVerified = "USER_ALREADY_VERIFIED";

    // Session
    public const string SessionNotFound = "SESSION_NOT_FOUND";
    public const string SessionExpired = "SESSION_EXPIRED";
    public const string SessionRevoked = "SESSION_REVOKED";
    public const string SessionAlreadyRevoked = "SESSION_ALREADY_REVOKED";

    // General
    public const string UnexpectedError = "UNEXPECTED_ERROR";

    // Token
    public const string TokenAlreadyUsed = "TOKEN_ALREADY_USED";
    public const string TokenExpired = "TOKEN_EXPIRED";

    // Verification

    public const string VerificationTokenNotFound = "VERIFICATION_TOKEN_NOT_FOUND";
    public const string VerificationTokenExpired = "VERIFICATION_TOKEN_EXPIRED";
    public const string VerificationTokenUsed = "VERIFICATION_TOKEN_USED";
    public const string VerificationTokenTypeMismatch = "VERIFICATION_TOKEN_TYPE_MISMATCH";
}