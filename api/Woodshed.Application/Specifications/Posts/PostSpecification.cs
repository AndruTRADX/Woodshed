using System;
using Woodshed.Domain;

namespace Woodshed.Application.Specifications.Posts;

public class PostSpecification : BaseSpecification<Post>
{
    public PostSpecification(PostSpecificationParams specParams, string? userId) : base(
        x =>
            (string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId.Equals(specParams.UserId))
            && (!specParams.IsMyPost || (!string.IsNullOrWhiteSpace(userId) && x.UserId.Equals(specParams.UserId)))
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        if (!string.IsNullOrWhiteSpace(specParams.Sort))
        {
            switch (specParams.Sort)
            {
                case "createdAt":
                    AddOrderBy(p => p.CreatedAt);
                    break;
                case "CreatedAtDesc":
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
                default:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
            }
        }
    }
}