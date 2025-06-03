using KSFramework.Messaging.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using KSFramework.Messaging.Behaviors;

namespace KSFramework.Messaging.Extensions;

/// <summary>
/// Extension methods for registering mediator handlers and behaviors.
/// </summary>
public static class RegisterMediatorServices
{
    /// <summary>
    /// Registers mediator handlers and behaviors from the specified assemblies.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="assemblies">The assemblies to scan.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddKSMediator(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<ISender>(sp => sp.GetRequiredService<IMediator>());

        services.Scan(scan => scan
            .FromAssemblies(assemblies)

            // Register request handlers
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            // Register notification handlers
            .AddClasses(c => c.AssignableTo(typeof(INotificationHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            // Register stream request handlers
            .AddClasses(c => c.AssignableTo(typeof(IStreamRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            // Register pipeline behaviors
            .AddClasses(c => c.AssignableTo(typeof(IPipelineBehavior<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            // Register notification behaviors
            .AddClasses(c => c.AssignableTo(typeof(INotificationBehavior<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

            // Register stream pipeline behaviors ✅
            .AddClasses(c => c.AssignableTo(typeof(IStreamPipelineBehavior<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
        );

        // If you have default behaviors (e.g., logging), register them here
        services.AddScoped(typeof(IStreamPipelineBehavior<,>), typeof(StreamLoggingBehavior<,>));

        return services;
    }
}