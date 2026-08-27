using FluentValidation;

namespace Woodshed.Application.Features.Posts.Commands.Create;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.Request.Content)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Content is required and must have at least 3 characters")
            .MaximumLength(3072)
            .WithMessage("Content must not exceed 3072 characters");
    }
}
