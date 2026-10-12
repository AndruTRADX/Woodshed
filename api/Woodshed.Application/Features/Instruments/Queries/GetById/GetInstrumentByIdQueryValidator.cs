using FluentValidation;

namespace Woodshed.Application.Features.Instruments.Queries.GetById;

public class GetInstrumentByIdQueryValidator : AbstractValidator<GetInstrumentByIdQuery>
{
    public GetInstrumentByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required")
            .MaximumLength(36)
            .WithMessage("Id must not exceed 36 characters");
    }
}
