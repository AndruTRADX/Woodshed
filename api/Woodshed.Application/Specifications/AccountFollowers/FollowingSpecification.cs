using Woodshed.Domain;

namespace Woodshed.Application.Specifications.AccountFollowers;

public class FollowingSpecification : BaseSpecification<UserFollower>
{
    public FollowingSpecification(AccountFollowerSpecificationParams specParams) : base(
        x => x.FollowerId == specParams.UserId
    )
    {
        ApplyPaging(specParams.PageSize * (specParams.PageIndex - 1), specParams.PageSize);

        AddInclude(x => x.Followee);
        AddOrderByDescending(x => x.FollowedAt);
    }
}
