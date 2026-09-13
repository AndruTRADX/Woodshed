using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Models.Response.Follow;

public class FollowerResponse
{
    public required DateTime FollowedAt { get; set; }
    public required UserAccountResponse Follower { get; set; }
}
