using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Domain;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDomain(this IServiceCollection services)
    {
        return services;
    }

    public static WebApplication UseDomain(this WebApplication app)
    {
        return app;
    }
}