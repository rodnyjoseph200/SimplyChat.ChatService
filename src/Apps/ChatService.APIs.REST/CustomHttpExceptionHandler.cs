using System.Net;
using ChatService.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ChatService.APIs.REST;

public class CustomHttpExceptionHandler : IExceptionHandler
{
    private readonly ILogger<CustomHttpExceptionHandler> _logger;
    public CustomHttpExceptionHandler(ILogger<CustomHttpExceptionHandler> logger) => _logger = logger;

    public ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        _ = exception switch
        {
            //todo handle multi-status exeption
            ResourceNotFoundException ex => HandleExceptionAsync(context, HttpStatusCode.NotFound, ex, "Resource not found", cancellationToken),
            ResourceAlreadyExistsException ex => HandleExceptionAsync(context, HttpStatusCode.Conflict, ex, "Resource already exists", cancellationToken),
            BadRequestException ex => HandleExceptionAsync(context, HttpStatusCode.BadRequest, ex, "Bad request", cancellationToken),
            _ => HandleExceptionAsync(context, HttpStatusCode.InternalServerError, exception, "Could not process request", cancellationToken),
        };
        return ValueTask.FromResult(true);
    }

    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, Exception ex, string title, CancellationToken cancellationToken)
    {
        _logger.LogError(ex, title);

        var problemDetails = new ProblemDetailsContext
        {
            HttpContext = context,
            ProblemDetails =
            {
                Status = (int?)statusCode,
                Title = title,
                Type = statusCode.ToString(),
                Detail = ex.Message
            }
        };

        context.Response.StatusCode = problemDetails.ProblemDetails.Status.Value;

        await context.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);
    }
}