using FluentValidation;

namespace Project.Application.Blog.CreatePost;

public sealed class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty()
            .Length(10, 500);

        RuleFor(x => x.Content)
            .NotNull()
            .NotEmpty();
    }
}