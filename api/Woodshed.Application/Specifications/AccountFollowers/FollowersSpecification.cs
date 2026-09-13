using Woodshed.Domain;

namespace Woodshed.Application.Specifications.AccountFollowers;

public class FollowersSpecification : BaseSpecification<UserFollower>
{
    public FollowersSpecification(AccountFollowerSpecificationParams specParams) : base(
        x => x.FolloweeId == specParams.UserId
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        AddInclude(x => x.Followee);
        AddOrderByDescending(x => x.FollowedAt);
    }
}
