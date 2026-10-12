using FluentValidation;

namespace Woodshed.Application.Features.UserInstruments.Queries.GetByUserId;

public class GetUserInstrumentsQueryValidator : AbstractValidator<GetUserInstrumentsQuery>
{
    public GetUserInstrumentsQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull().NotEmpty()
            .WithMessage("Id is required")
            .MaximumLength(36)
            .WithMessage("Id must not exceed 36 characters");
    }
}
