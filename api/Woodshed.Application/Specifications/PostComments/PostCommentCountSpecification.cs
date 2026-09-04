using Woodshed.Domain;

namespace Woodshed.Application.Specifications.PostComments;

public class PostCommentCountSpecification : BaseSpecification<PostComment>
{
    public PostCommentCountSpecification(PostCommentSpecificationParams specParams, string postId) : base(
        x =>
            x.PostId.Equals(postId) &&
            (string.IsNullOrWhiteSpace(specParams.UserId) || x.UserId.Equals(specParams.UserId))
    )
    {

    }
}