using Woodshed.Domain;

namespace Woodshed.Application.Specifications.AccountFollowers;

public class FollowersCountSpecification(AccountFollowerSpecificationParams specParams) : BaseSpecification<UserFollower>(
    x => x.FolloweeId == specParams.UserId
)
{ }
