using System;
using FluentValidation;

namespace Woodshed.Application.Features.PostLikes.Commands.Delete;

public class DeletePostLikeCommandValidator : AbstractValidator<DeletePostLikeCommand>
{
    public DeletePostLikeCommandValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull().NotEmpty()
            .WithMessage("PostId is required")
            .MaximumLength(36)
            .WithMessage("PostId must not exceed 36 characters");
    }
}
