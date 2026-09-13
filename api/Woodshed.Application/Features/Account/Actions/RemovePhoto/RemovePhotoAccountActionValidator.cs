using FluentValidation;

namespace Woodshed.Application.Features.Account.Actions.RemovePhoto;

public class RemovePhotoAccountActionValidator : AbstractValidator<RemovePhotoAccountAction>
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
