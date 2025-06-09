using KSFramework.KSMessaging.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Blog.CreatePost;

namespace Project.Presentation.Controllers;

public sealed class PostsController(ISender sender) : BaseController(sender)
{
    [HttpPost]
    [Route(Routes.Posts.CreatePost)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreatePostResponse),
        StatusCodes.Status200OK)]
    public async Task<ActionResult<CreatePostResponse>> PostAsync(
        [FromForm] CreatePostRequest request,
        CancellationToken cancellationToken = default)
    {
        CreatePostCommand command = new()
        {
            Title = request.Title,
            Content = request.Content
        };

        CreatePostResponse result = await Sender.Send(command,
            cancellationToken);

        return Ok(result);
    }
}