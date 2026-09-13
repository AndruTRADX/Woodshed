using Woodshed.Domain;

namespace Woodshed.Application.Specifications.AccountFollowers;

public class FollowersSpecification : BaseSpecification<UserFollower>
{
    public FollowersSpecification(AccountFollowerSpecificationParams specParams) : base(
        x => x.FolloweeId == specParams.UserId
    )
    {
        AddInclude(x => x.Follower);
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);
    }
}
