using FluentValidation;
using Woodshed.Application.Specifications;

namespace Woodshed.Application.Features.Account.Queries.GetPagedFollowing;

public class GetPagedFollowingQueryValidator : SpecificationParamsValidator<GetPagedFollowingQuery>
{
    public GetPagedFollowingQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is Required.")
            .Length(36)
            .WithMessage("UserId must be 36 characters");
    }
}
