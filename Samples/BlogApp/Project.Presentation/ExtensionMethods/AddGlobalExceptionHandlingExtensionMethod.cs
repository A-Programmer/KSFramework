using Microsoft.Extensions.DependencyInjection;
using Project.Presentation.Handlers;

namespace Project.Presentation.ExtensionMethods;

public static class AddGlobalExceptionHandlingExtensionMethod
{
    public static IServiceCollection AddGlobalExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails(opt =>
        {
            opt.CustomizeProblemDetails = ctx =>
            {
                ctx.ProblemDetails.Extensions.Add("trace-id", ctx.HttpContext.TraceIdentifier);
                ctx.ProblemDetails.Extensions.Add("address", $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}");
                ctx.ProblemDetails.Extensions.Add("Server-Name", Environment.MachineName);
            };
        });
        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }
}