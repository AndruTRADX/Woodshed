using FluentValidation;

namespace Woodshed.Application.Features.UserInstruments.Commands.Update;

public class UpdateUserInstrumentCommandValidator : AbstractValidator<UpdateUserInstrumentCommand>
{
    public UpdateUserInstrumentCommandValidator()
    {
        RuleFor(x => x.InstrumentId)
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
