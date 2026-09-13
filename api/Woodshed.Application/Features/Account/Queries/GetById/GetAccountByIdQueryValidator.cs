using FluentValidation;

namespace Woodshed.Application.Features.Account.Queries.GetById;

public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
{
    public GetAccountByIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is Required.")
            .Length(36)
            .WithMessage("UserId must be 36 characters");
    }
}
