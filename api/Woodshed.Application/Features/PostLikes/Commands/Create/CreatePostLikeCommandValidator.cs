using FluentValidation;

namespace Woodshed.Application.Features.PostLikes.Commands.Create;

public class CreatePostLikeCommandValidator : AbstractValidator<CreatePostLikeCommand>
{
    public CreatePostLikeCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull().NotEmpty()
            .WithMessage("PostId is required")
            .MaximumLength(36)
            .WithMessage("PostId must not exceed 36 characters");
    }
}