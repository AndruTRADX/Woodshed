using FluentValidation;

namespace Woodshed.Application.Features.Account.Actions.Unfollow;

public class UnfollowAccountActionValidator : AbstractValidator<UnfollowAccountAction>
{
    public UnfollowAccountActionValidator()
    {
        RuleFor(x => x.TargetUserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("TargetUserId is Required.")
            .Length(36)
            .WithMessage("TargetUserId must be 36 characters");
    }
}
