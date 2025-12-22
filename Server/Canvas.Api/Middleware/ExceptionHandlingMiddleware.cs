using System.Net;
using System.Text.Json;
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
        var (status, message) = ex switch
        {
            UserNotFoundException e => (HttpStatusCode.NotFound, e.Message),
            EmailAlreadyInUseException e => (HttpStatusCode.Conflict, e.Message),
            InvalidEmailException e => (HttpStatusCode.BadRequest, e.Message),


            _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        var payload = JsonSerializer.Serialize(new
        {
            error = message,
        });

        return context.Response.WriteAsync(payload);
    }
}