// using KSFramework.Messaging.Abstraction;
// using Microsoft.Extensions.DependencyInjection;
// using System.Reflection;
//
// namespace KSFramework.Messaging.Configuration
// {
//     /// <summary>
//     /// Extension methods to register messaging components into the service collection.
//     /// </summary>
//     public static class ServiceCollectionExtensions
//     {
//         /// <summary>
//         /// Registers mediator handlers and behaviors from specified assemblies with automatic discovery.
//         /// </summary>
//         public static IServiceCollection AddKSMediator(this IServiceCollection services, params Assembly[] assemblies)
//         {
//             services.AddScoped<IMediator, Mediator>();
//             services.AddScoped<ISender>(sp => sp.GetRequiredService<IMediator>());
//
//             services.Scan(scan => scan
//                 .FromAssemblies(assemblies)
//
//                 // Request Handlers
//                 .AddClasses(c => c.AssignableTo(typeof(IRequestHandler<,>)))
//                 .AsImplementedInterfaces()
//                 .WithScopedLifetime()
//
//                 // Notification Handlers
//                 .AddClasses(c => c.AssignableTo(typeof(INotificationHandler<>)))
//                 .AsImplementedInterfaces()
//                 .WithScopedLifetime()
//
//                 // Pipeline Behaviors
//                 .AddClasses(c => c.AssignableTo(typeof(IPipelineBehavior<,>)))
//                 .AsImplementedInterfaces()
//                 .WithScopedLifetime()
//
//                 // Notification Behaviors
//                 .AddClasses(c => c.AssignableTo(typeof(INotificationBehavior<>)))
//                 .AsImplementedInterfaces()
//                 .WithScopedLifetime()
//             );
//
//             return services;
//         }
//     }
// }