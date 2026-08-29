using Woodshed.Domain;

namespace Woodshed.Application.Specifications.Posts;

public class PostCountSpecification(PostSpecificationParams specParams, string? userId) : BaseSpecification<Post>(
    x =>
        (string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId.Equals(specParams.UserId))
        && (!specParams.IsMyPost || (!string.IsNullOrWhiteSpace(userId) && x.UserId.Equals(specParams.UserId)))
    )
{ }