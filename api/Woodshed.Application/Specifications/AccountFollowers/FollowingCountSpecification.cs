using Woodshed.Domain;

namespace Woodshed.Application.Specifications.AccountFollowers;

public class FollowingCountSpecification(AccountFollowerSpecificationParams specParams) : BaseSpecification<UserFollower>(
    x => x.FollowerId == specParams.UserId
)
{ }
