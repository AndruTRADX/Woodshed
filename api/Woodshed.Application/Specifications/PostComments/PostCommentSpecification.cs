using Woodshed.Domain;

namespace Woodshed.Application.Specifications.PostComments;

public class PostCommentSpecification : BaseSpecification<PostComment>
{
    public PostCommentSpecification(PostCommentSpecificationParams specParams, string postId) : base(
        x =>
            x.PostId.Equals(postId) &&
            (string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId.Equals(specParams.UserId))
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        AddInclude(x => x.User);

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