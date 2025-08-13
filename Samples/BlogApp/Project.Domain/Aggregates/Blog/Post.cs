using System.Runtime.Serialization;
using KSFramework.KSDomain;
using KSFramework.KSDomain.AggregatesHelper;
using KSFramework.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Aggregates.Blog.Events;

namespace Project.Domain.Aggregates.Blog;

public class Post : BaseEntity, IAggregateRoot, ISerializable
{
    protected Post(Guid id)
        :base(id)
    {
    }

    public string Title { get; private set; }

    public string Content { get; private set; }

    public string Slug { get; private set; }
    
    private readonly List<Category> _categories;
    protected IReadOnlyCollection<Category> Categories => _categories;
    
    private readonly List<Comment> _comments;
    public IReadOnlyCollection<Comment> Comments => _comments;
    
    public int CommentsCount => _comments.Count;

    private Post( string title, string content)
    {
        Title = title;
        Content = content;
        Slug = title.CreateSlug();
        _categories = new List<Category>();
        _comments = new List<Comment>();
    }

    public static Post Create(string title, string content)
    {
        Post post = new(title, content)
        {
            Id = Guid.NewGuid(),
        };
        
        post.AddDomainEvent(new PostCreatedDomainEvent(post.Id, post.Title));
        return post;
    }

    public void Update(string title, string content)
    {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public void UpdateSlug(string newSlug)
    {
        Slug = newSlug ?? throw new ArgumentNullException(nameof(newSlug));
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public void RemoveComment(Comment comment)
    {
        _comments.Remove(comment);
    }
    
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
        info.AddValue(nameof(Title), Title);
        info.AddValue(nameof(Content), Content);
    }
}

public sealed class PostsConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(a => a.Comments)
            .WithOne(ac => ac.Post)
            .HasForeignKey(ac => ac.PostId);
    }
}