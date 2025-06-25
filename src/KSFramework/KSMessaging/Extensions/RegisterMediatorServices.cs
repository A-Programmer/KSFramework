using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using KSFramework.Contracts;
using KSFramework.KSMessaging.Behaviors;

namespace KSFramework.KSMessaging.Extensions;

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
    public static IServiceCollection AddKSFramework(this IServiceCollection services, params Assembly[] assemblies)
    {
        services.AddScoped<IMediator, Mediator>();
        services.AddScoped<ISender>(sp => sp.GetRequiredService<IMediator>());
        
        services.RegisterAllImplementations<IInjectable>(assemblies);
        services.RegisterAllImplementationsOf<IInjectableWithImplementation>(assemblies);

        services.Scan(scan => scan
            .FromAssemblies(assemblies)

            // Register request handlers
            .AddClasses(c => c.AssignableTo(typeof(IRequest<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            // Register request handlers
            .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            // Register request handlers
            .AddClasses(c => c.AssignableTo(typeof(ICommand<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            // Register request handlers
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
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
    
    private static IServiceCollection RegisterAllImplementationsOf<TInterface>(this IServiceCollection services,
        Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var interfaceType = typeof(TInterface);

        foreach (var assembly in assemblies)
        {
            var implementations = assembly.DefinedTypes
                .Where(type => type.IsClass && !type.IsAbstract && interfaceType.IsAssignableFrom(type))
                .ToList();

            foreach (var implementation in implementations)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient(interfaceType, implementation);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(interfaceType, implementation);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(interfaceType, implementation);
                        break;
                    default:
                        throw new ArgumentException("Invalid service lifetime", nameof(lifetime));
                }
            }
        }

        return services;
    }
    
    private static IServiceCollection RegisterAllImplementations<TInterface>(this IServiceCollection services,
        Assembly[] assemblies,
        ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        var interfaceType = typeof(TInterface);

        foreach (var assembly in assemblies)
        {
            var implementations = assembly.DefinedTypes
                .Where(type => type.IsClass && !type.IsAbstract && interfaceType.IsAssignableFrom(type))
                .ToList();

            foreach (var implementation in implementations)
            {
                switch (lifetime)
                {
                    case ServiceLifetime.Transient:
                        services.AddTransient(implementation);
                        break;
                    case ServiceLifetime.Scoped:
                        services.AddScoped(implementation);
                        break;
                    case ServiceLifetime.Singleton:
                        services.AddSingleton(implementation);
                        break;
                    default:
                        throw new ArgumentException("Invalid service lifetime", nameof(lifetime));
                }
            }
        }

        return services;
    }
}