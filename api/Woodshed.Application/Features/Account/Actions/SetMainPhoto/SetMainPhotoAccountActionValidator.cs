using FluentValidation;

namespace Woodshed.Application.Features.Account.Actions.SetMainPhoto;

public class SetMainPhotoAccountActionValidator : AbstractValidator<SetMainPhotoAccountAction>
{
    public SetMainPhotoAccountActionValidator()
    {
        RuleFor(x => x.PhotoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhotoId is Required.")
            .Length(36)
            .WithMessage("PhotoId must be 36 characters");
    }
}