using FluentValidation;
using Woodshed.Application.Specifications;

namespace Woodshed.Application.Features.Account.Queries.GetPagePhotos;

public class GetPagedAccountPhotosQueryValidator : SpecificationParamsValidator<GetPagedAccountPhotosQuery>
{
    public GetPagedAccountPhotosQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is Required.")
            .Length(36)
            .WithMessage("UserId must be 36 characters");
    }
}
