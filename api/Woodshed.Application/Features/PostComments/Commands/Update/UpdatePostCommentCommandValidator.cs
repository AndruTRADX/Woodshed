using FluentValidation;

namespace Woodshed.Application.Features.PostComments.Commands.Update;

public class UpdatePostCommentCommandValidator : AbstractValidator<UpdatePostCommentCommand>
{
    public UpdatePostCommentCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull().NotEmpty()
            .WithMessage("PostId is required")
            .Length(36)
            .WithMessage("PostId must be 36 characters");

        RuleFor(x => x.CommentId)
            .NotNull().NotEmpty()
            .WithMessage("CommentId is required")
            .Length(36)
            .WithMessage("CommentId must be 36 characters");

        RuleFor(x => x.Request.Content)
            .NotNull().NotEmpty()
            .WithMessage("Content is required")
            .MaximumLength(1024)
            .WithMessage("Content must not exceed 1024 characters");
    }
}