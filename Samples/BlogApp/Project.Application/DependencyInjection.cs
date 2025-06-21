using KSFramework.KSMessaging.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Application;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        services.AddKSFramework(Project.Application.AssemblyReference.Assembly);
        return services;
    }

    public static WebApplication UseApplication(this WebApplication app)
    {
        return app;
    }
}
