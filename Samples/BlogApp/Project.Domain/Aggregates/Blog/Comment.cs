using System.Runtime.Serialization;
using KSFramework.KSDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Domain.Aggregates.Blog;

public class Comment : Entity, ISerializable
{
    public string FullName { get; private set; }
    public string Content { get; private set; }
    
    public Guid PostId { get; private set; }
    public Post Post { get; private set; }
    
    private Comment(Guid id,
        string fullName,
        string content)
        : base(id)
    {
        FullName = fullName ?? throw new ArgumentException($"{nameof(fullName)}");
        Content = content ?? throw new ArgumentException($"{nameof(content)}");
    }

    public static Comment Create(string fullName,
        string content) =>
        new(Guid.NewGuid(),
            fullName,
            content);
    
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Id), Id);
        info.AddValue(nameof(FullName), FullName);
        info.AddValue(nameof(Content), Content);
    }
}

public sealed class CommentsConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(x => x.Id);
    }
}