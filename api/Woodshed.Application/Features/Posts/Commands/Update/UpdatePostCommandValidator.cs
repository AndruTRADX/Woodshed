using FluentValidation;

namespace Woodshed.Application.Features.Posts.Commands.Update;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required")
            .MaximumLength(36)
            .WithMessage("Id must not exceed 36 characters");

        RuleFor(x => x.Request.Content)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Content is required and must have at least 3 characters")
            .MaximumLength(3072)
            .WithMessage("Content must not exceed 3072 characters");
    }
}
