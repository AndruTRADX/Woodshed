using FluentValidation;

namespace Woodshed.Application.Features.PostComments.Commands.Delete;

public class DeletePostCommentCommandValidator: AbstractValidator<DeletePostCommentCommand>
{
    public DeletePostCommentCommandValidator()
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
    }
}