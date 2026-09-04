using FluentValidation;

namespace Woodshed.Application.Features.PostComments.Queries.GetPaged;

public class GetPagedPostCommentQueryValidator : AbstractValidator<GetPagedPostCommentQuery>
{
    public GetPagedPostCommentQueryValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull().NotEmpty()
            .WithMessage("PostId is required")
            .MaximumLength(36)
            .WithMessage("PostId must not exceed 36 characters");
    }
}
