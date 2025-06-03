using KSFramework.Messaging.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.Messaging;

/// <summary>
/// The default implementation of the mediator pattern.
/// </summary>
public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="Mediator"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider to resolve handlers and behaviors.</param>
    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc/>
    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();
        var responseType = typeof(TResponse);

        // Get the handler type
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);
        var handler = _serviceProvider.GetService(handlerType);
        if (handler == null)
            throw new InvalidOperationException($"Handler for '{requestType.Name}' not found.");

        // Get all pipeline behaviors for this request/response
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(requestType, responseType);
        var behaviors = _serviceProvider.GetServices(behaviorType).Cast<object>().Reverse().ToList();

        // Create the final handler delegate
        RequestHandlerDelegate<TResponse> handlerDelegate = () =>
        {
            var method = handlerType.GetMethod("Handle");
            return (Task<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken });
        };

        // Compose the pipeline by wrapping behaviors around the handler delegate
        foreach (var behavior in behaviors)
        {
            var currentBehavior = behavior;
            var next = handlerDelegate;
            handlerDelegate = () =>
            {
                var method = behaviorType.GetMethod("Handle");
                return (Task<TResponse>)method.Invoke(currentBehavior, new object[] { request, cancellationToken, next });
            };
        }

        // Execute the composed pipeline
        return await handlerDelegate();
    }
    
    public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        var requestType = request.GetType();

        var requestInterface = requestType
            .GetInterfaces()
            .FirstOrDefault(i =>
                i.IsGenericType &&
                i.GetGenericTypeDefinition() == typeof(IRequest<>));

        if (requestInterface == null)
            throw new InvalidOperationException("Request does not implement IRequest<>");

        var responseType = requestInterface.GetGenericArguments()[0];

        var method = typeof(Mediator)
            .GetMethods()
            .FirstOrDefault(m =>
                m.Name == nameof(Send)
                && m.IsGenericMethodDefinition
                && m.GetGenericArguments().Length == 1
                && m.GetParameters().Length == 2
                && m.GetParameters()[0].ParameterType.IsGenericType
                && m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IRequest<>));

        if (method == null)
            throw new InvalidOperationException("Unable to find the correct generic Send method.");

        var genericMethod = method.MakeGenericMethod(responseType);

        var task = (Task)genericMethod.Invoke(this, new object[] { request, cancellationToken })!;
        await task.ConfigureAwait(false);

        var resultProperty = task.GetType().GetProperty("Result");
        return resultProperty?.GetValue(task);
    }
    
    /// <inheritdoc/>
    public async Task Publish(object notification, CancellationToken cancellationToken = default)
    {
        if (notification == null)
            throw new ArgumentNullException(nameof(notification));

        var notificationType = notification.GetType();

        if (!typeof(INotification).IsAssignableFrom(notificationType))
            throw new InvalidOperationException($"'{notificationType.Name}' does not implement INotification.");

        var method = typeof(Mediator)
            .GetMethods()
            .FirstOrDefault(m =>
                m.Name == nameof(Publish) &&
                m.IsGenericMethodDefinition &&
                m.GetGenericArguments().Length == 1 &&
                m.GetParameters().Length == 2);

        if (method == null)
            throw new InvalidOperationException("Unable to find generic Publish method.");

        var genericMethod = method.MakeGenericMethod(notificationType);
        await (Task)genericMethod.Invoke(this, new object[] { notification, cancellationToken })!;
    }
    
    // /// <inheritdoc/>
    // public async Task<object?> Send(object request, CancellationToken cancellationToken = default)
    // {
    //     if (request == null)
    //         throw new ArgumentNullException(nameof(request));
    //
    //     var requestType = request.GetType();
    //
    //     // پیدا کردن اینترفیس IRequest<TResponse> که روی request اعمال شده
    //     var interfaceType = requestType
    //         .GetInterfaces()
    //         .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>));
    //
    //     if (interfaceType == null)
    //         throw new InvalidOperationException($"Request type '{requestType.Name}' does not implement IRequest<> interface.");
    //
    //     var responseType = interfaceType.GetGenericArguments()[0];
    //
    //     // ساختن متد جنریک Send<TResponse>(IRequest<TResponse>)
    //     var method = typeof(IMediator)
    //         .GetMethods()
    //         .FirstOrDefault(m =>
    //             m.Name == nameof(Send) &&
    //             m.IsGenericMethodDefinition &&
    //             m.GetParameters().Length == 2 &&
    //             m.GetParameters()[0].ParameterType.GetGenericTypeDefinition() == typeof(IRequest<>));
    //
    //     if (method == null)
    //         throw new InvalidOperationException("Unable to find generic Send method.");
    //
    //     var genericMethod = method.MakeGenericMethod(responseType);
    //
    //     return await (Task<object?>)genericMethod.Invoke(this, new object[] { request, cancellationToken });
    // }


    /// <summary>
    /// Publishes a notification to all registered handlers.
    /// </summary>
    /// <typeparam name="TNotification">The notification type.</typeparam>
    /// <param name="notification">The notification instance.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification
    {
        var notificationType = typeof(TNotification);

        var behaviorType = typeof(INotificationBehavior<>).MakeGenericType(notificationType);
        var behaviors = _serviceProvider.GetServices(behaviorType).Cast<object>().Reverse().ToList();

        var handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>().ToList();

        if (!handlers.Any())
            return;

        NotificationHandlerDelegate handlerDelegate = () =>
        {
            var tasks = handlers.Select(h => h.Handle(notification, cancellationToken));
            return Task.WhenAll(tasks);
        };

        foreach (var behavior in behaviors)
        {
            var currentBehavior = behavior;
            var next = handlerDelegate;
            handlerDelegate = () =>
            {
                var method = behavior.GetType().GetMethod("Handle");
                return (Task)method.Invoke(currentBehavior, new object[] { notification, cancellationToken, next });
            };
        }

        await handlerDelegate();
    }
    
    public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IStreamRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var handler = _serviceProvider.GetService(handlerType);

        if (handler == null)
            throw new InvalidOperationException($"No stream handler registered for {request.GetType().Name}");

        var method = handlerType.GetMethod("Handle");

        var result = (IAsyncEnumerable<TResponse>)method.Invoke(handler, new object[] { request, cancellationToken });

        return result;
    }
}