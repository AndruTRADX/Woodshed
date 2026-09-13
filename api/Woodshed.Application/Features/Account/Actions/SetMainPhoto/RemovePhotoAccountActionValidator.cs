using FluentValidation;

namespace Woodshed.Application.Features.Account.Actions.SetMainPhoto;

public class RemovePhotoAccountActionValidator : AbstractValidator<SetMainPhotoAccountAction>
{
    public RemovePhotoAccountActionValidator()
    {
        RuleFor(x => x.PhotoId)
            .NotNull()
            .NotEmpty()
            .WithMessage("PhotoId is Required.")
            .Length(36)
            .WithMessage("PhotoId must be 36 characters");
    }
}