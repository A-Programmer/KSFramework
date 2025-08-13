using KSFramework.KSDomain;

namespace Project.Domain.Aggregates.Blog.Events;

public sealed class PostCreatedDomainEvent : IDomainEvent
{
    public PostCreatedDomainEvent(Guid postId, string title)
    {
        PostId = postId;
        Title = title;
        OccurredOn = DateTime.UtcNow;
    }

    public Guid PostId { get; }
    public string Title { get; }
    public DateTime OccurredOn { get; }
}