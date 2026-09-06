using System;
using Woodshed.Domain;

namespace Woodshed.Application.Specifications.PostLikes;

public class PostLikeCountSpecification : BaseSpecification<PostLike>
{
    public PostLikeCountSpecification(PostLikeSpecificationParams specParams, string postId) : base(
        x =>
            x.PostId.Equals(postId)
    )
    {
        
    }
}