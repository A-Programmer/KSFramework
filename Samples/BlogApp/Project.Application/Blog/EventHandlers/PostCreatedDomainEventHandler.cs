using KSFramework.KSMessaging.Abstraction;
using Microsoft.Extensions.Logging;
using Project.Domain.Aggregates.Blog.Events;

namespace Project.Application.Blog.EventHandlers;

public sealed class PostCreatedDomainEventHandler : INotificationHandler<PostCreatedDomainEvent>
{
    private readonly ILogger<PostCreatedDomainEventHandler> _logger;

    public PostCreatedDomainEventHandler(ILogger<PostCreatedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(PostCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Post created: {PostId} - {Title}", notification.PostId, notification.Title);
        
        // Here you can add additional business logic like:
        // - Sending notifications
        // - Updating read models
        // - Triggering external systems
        
        return Task.CompletedTask;
    }
}