using System.Net;
using System.Text.Json;
using Canvas.Domain.Exceptions;
using Canvas.Domain.Errors;

using Canvas.Application.Exceptions;
using Canvas.Application.Auth.Exceptions;

namespace Canvas.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var (status, code, message) = ex switch
        {
            // Services.Exceptions
            UserNotFoundException e => (
                HttpStatusCode.NotFound,
                ErrorCodes.UserNotFound,
                e.Message
            ),
            EmailAlreadyInUseException e => (
                HttpStatusCode.Conflict,
                ErrorCodes.EmailAlreadyInUse,
                e.Message
            ),
            InvalidPasswordException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.InvalidPassword,
                e.Message
            ),
            SessionNotFoundException e => (
                HttpStatusCode.NotFound,
                ErrorCodes.SessionNotFound,
                e.Message
            ),
            SessionExpiredException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.SessionExpired,
                e.Message
            ),
            SessionRevokedException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.SessionRevoked,
                e.Message
            ),
            SessionAlreadyRevokedException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.SessionAlreadyRevoked,
                e.Message
            ),
            VerificationTokenNotFoundException e => (
                HttpStatusCode.NotFound,
                ErrorCodes.VerificationTokenNotFound,
                e.Message
            ),
            VerificationTokenExpiredException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.VerificationTokenExpired,
                e.Message
            ),
            VerificationTokenUsedException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.VerificationTokenUsed,
                e.Message
            ),
            UserAlreadyVerifiedException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.UserAlreadyVerified,
                e.Message
            ),
            UserNotVerifiedException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.UserNotVerified,
                e.Message
            ),
            VerificationEmailRateLimitException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.VerificationEmailRateLimit,
                e.Message
            ),
            // Domain.Exceptions
            InvalidEmailException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.InvalidEmail,
                e.Message
            ),

            InvalidPasswordHashException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.InvalidPasswordHash,
                e.Message
            ),

            EmailUnchangedException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.EmailUnchanged,
                e.Message
                ),

            InvalidBoardTitleException e => (
                HttpStatusCode.BadRequest,
                ErrorCodes.InvalidBoardTitle,
                e.Message
            ),

            _ => (
                HttpStatusCode.InternalServerError,
                ErrorCodes.UnexpectedError,
                "An unexpected error occurred"
            )
        };

        if (context.Response.HasStarted)
            throw new InvalidOperationException("The response has already started, so the exception cannot be handled.");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var payload = JsonSerializer.Serialize(new
        {
            error = new
            {
                code = code,
                message = message,
            },
            timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
        });

        return context.Response.WriteAsync(payload);
    }
}