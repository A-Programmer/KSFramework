using System.Net;
using FluentValidation;
using KSFramework.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Project.Presentation.Handlers;

public class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "An unexpected error occured");

        var statusCode = exception switch
        {
            KSNotFoundException => (int) HttpStatusCode.NotFound,
            ValidationException => (int) HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };
        
        httpContext.Response.StatusCode = statusCode;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Status = statusCode,
                Title = "An error occurred",
                Detail = exception.Message,
                Type = exception.GetType().Name,
            },
            Exception = exception
        });
    }
}