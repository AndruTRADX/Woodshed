using FluentValidation;

namespace Woodshed.Application.Features.UserInstruments.Commands.Create;

public class CreateUserInstrumentCommandValidator : AbstractValidator<CreateUserInstrumentCommand>
{
    public CreateUserInstrumentCommandValidator()
    {
        RuleFor(x => x.Request.InstrumentId)
            .NotNull().NotEmpty()
            .WithMessage("InstrumentId is required")
            .MaximumLength(36)
            .WithMessage("InstrumentId must not exceed 36 characters");

        RuleFor(x => x.Request.ProficiencyId)
            .IsInEnum()
            .WithMessage("ProficiencyId is required");

        RuleFor(x => x.Request.StartedPlayingAt)
            .NotEmpty()
            .LessThanOrEqualTo(_ => DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Date cannot be greater than today");
    }
}
