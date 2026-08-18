using FluentValidation;

namespace Woodshed.Application.Specifications;

public abstract class SpecificationParamsValidator<T> : AbstractValidator<T> where T : SpecificationParams
{
    protected SpecificationParamsValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0)
            .WithMessage("PageIndex must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 50)
            .WithMessage("PageSize must be between 1 and 50");

        RuleFor(x => x.Search)
            .MaximumLength(1000)
            .WithMessage("Search must not exceed 1000 characters");

        RuleFor(x => x.Sort)
            .MaximumLength(126)
            .WithMessage("Sort must not exceed 126 characters");
    }
}