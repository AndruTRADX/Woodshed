using FluentValidation;

namespace Woodshed.Application.Features.Account.Actions.FollowAccount;

public class FollowAccountActionValidator : AbstractValidator<FollowAccountAction>
{
    public FollowAccountActionValidator()
    {
        RuleFor(x => x.TargetUserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("TargetUserId is Required.")
            .Length(36)
            .WithMessage("TargetUserId must be 36 characters");
    }
}
