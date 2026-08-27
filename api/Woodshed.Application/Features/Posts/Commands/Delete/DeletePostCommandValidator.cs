using FluentValidation;

namespace Woodshed.Application.Features.Posts.Commands.Delete;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required")
            .MaximumLength(36)
            .WithMessage("Id must not exceed 36 characters");
    }
}
