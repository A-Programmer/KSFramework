using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace KSFramework.KSDomain;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchEventsAsync(IEnumerable<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(INotificationHandler<>).MakeGenericType(domainEvent.GetType());
            
            using var scope = _serviceProvider.CreateScope();
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await (Task)handlerType
                    .GetMethod("Handle")
                    ?.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
            }
        }
    }
}