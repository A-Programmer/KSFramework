using KSFramework.KSMessaging.Abstraction;

namespace Project.Application.Blog.CreatePost;

public record CreatePostCommand : ICommand<CreatePostResponse>
{
    public required string Title { get; set; }
    public required string Content { get; set; }
}