using System.Net;
using System.Text.Json;
using Canvas.Api.Data.Exceptions;
using Canvas.Api.Errors;
using Canvas.Api.Services.User.Exceptions;

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

            // Data.Exceptions
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