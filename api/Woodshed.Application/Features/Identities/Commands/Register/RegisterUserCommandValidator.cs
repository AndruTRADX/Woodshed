using FluentValidation;

namespace Woodshed.Application.Features.Identities.Commands.Register;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Account.Email)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("Email is required and must have at least 3 characters")
            .MaximumLength(256)
            .WithMessage("Email must not exceed 256 characters")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            .WithMessage("Must be a valid email");

        RuleFor(x => x.Account.Password)
            .NotNull().NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password is required and must have at least 3 characters")
            .MaximumLength(512)
            .WithMessage("Password must not exceed 512 characters");

        RuleFor(x => x.Account.NickName)
            .NotNull().NotEmpty()
            .MinimumLength(3)
            .WithMessage("NickName is required and must have at least 3 characters")
            .MaximumLength(64)
            .WithMessage("NickName must not exceed 64 characters");

        RuleFor(x => x.Account.Name)
            .MaximumLength(155)
            .WithMessage("Name must not exceed 155 characters");

        RuleFor(x => x.Account.LastName)
            .MaximumLength(155)
            .WithMessage("LastName must not exceed 155 characters");

        RuleFor(x => x.Account.Biography)
            .MaximumLength(512)
            .WithMessage("Biography must not exceed 512 characters");

        RuleFor(x => x.Account.ImageUrl)
            .MaximumLength(512)
            .WithMessage("ImageUrl must not exceed 512 characters");
    }
}
