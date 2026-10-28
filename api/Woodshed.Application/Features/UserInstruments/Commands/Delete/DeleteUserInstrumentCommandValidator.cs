using FluentValidation;

namespace Woodshed.Application.Features.UserInstruments.Commands.Delete;

public class DeleteUserInstrumentCommandValidator : AbstractValidator<DeleteUserInstrumentCommand>
{
    public DeleteUserInstrumentCommandValidator()
    {
        RuleFor(x => x.InstrumentId)
            .NotNull().NotEmpty()
            .WithMessage("InstrumentId is required")
            .MaximumLength(36)
            .WithMessage("InstrumentId must not exceed 36 characters");
    }
}
