using Woodshed.Domain;

namespace Woodshed.Application.Specifications.PostLikes;

public class PostLikeSpecification : BaseSpecification<PostLike>
{
    public PostLikeSpecification(PostLikeSpecificationParams specParams, string postId) : base(
        x =>
            x.PostId.Equals(postId)
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
                    AddOrderBy(p => p.CreatedAt);
                    break;
            }
        } 
        else
        {
            AddOrderBy(p => p.CreatedAt);
        }
    }
}