using KSFramework.GenericRepository;
using KSFramework.KSMessaging;
using KSFramework.KSMessaging.Abstraction;
using Project.Domain.Aggregates.Blog;

namespace Project.Application.Blog.CreatePost;

public sealed class CreatePostCommandHandler(IUnitOfWork unitOfWork) : CqrsBase(unitOfWork),
    ICommandHandler<CreatePostCommand, CreatePostResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<CreatePostResponse> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        Post post = Post.Create(request.Title, request.Content);
        await _unitOfWork.GetRepository<Post>()
            .AddAsync(post);
        await _unitOfWork.SaveChangesAsync();

        return new CreatePostResponse(post.Id, post.Slug);
    }
}