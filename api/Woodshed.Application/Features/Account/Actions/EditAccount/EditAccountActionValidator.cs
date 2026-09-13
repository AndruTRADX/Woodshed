using FluentValidation;

namespace Woodshed.Application.Features.Account.Actions.EditAccount;

public class EditAccountActionValidator : AbstractValidator<EditAccountAction>
{
    public EditAccountActionValidator()
    {
        RuleFor(x => x.Request.NickName)
            .NotEmpty().WithMessage("Nickname is required.")
            .MinimumLength(3).WithMessage("Nickname must be at least 3 characters long.")
            .MaximumLength(64).WithMessage("Nickname cannot exceed 64 characters.")
            .Matches("^[a-zA-Z0-9_.]+$").WithMessage("Nickname can only contain letters, numbers, dots, and underscores.");

        RuleFor(x => x.Request.Name)
            .MaximumLength(155).WithMessage("Name cannot exceed 155 characters.");

        RuleFor(x => x.Request.LastName)
            .MaximumLength(155).WithMessage("Last name cannot exceed 155 characters.");

        RuleFor(x => x.Request.Biography)
            .MaximumLength(1024).WithMessage("Biography cannot exceed 1024 characters.");
    }
}