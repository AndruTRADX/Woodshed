using FluentValidation;

namespace Woodshed.Application.Features.PostComments.Commands.Create;

public class CreatePostCommentCommandValidator : AbstractValidator<CreatePostCommentCommand>
{
    public CreatePostCommentCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull().NotEmpty()
            .WithMessage("PostId is required")
            .MaximumLength(36)
            .WithMessage("PostId must not exceed 36 characters");

        RuleFor(x => x.Request.Content)
            .NotNull().NotEmpty()
            .WithMessage("Content is required")
            .MaximumLength(1024)
            .WithMessage("Content must not exceed 1024 characters");
    }
}
