using FluentValidation;

namespace Woodshed.Application.Features.Posts.Queries.GetById;

public class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
{
    public GetPostByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().NotEmpty()
            .WithMessage("Id is required")
            .MaximumLength(36)
            .WithMessage("Id must not exceed 36 characters");
    }
}
