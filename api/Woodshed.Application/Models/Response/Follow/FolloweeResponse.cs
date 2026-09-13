using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Models.Response.Follow;

public class FolloweeResponse
{
    public required DateTime FollowedAt { get; set; }
    public required UserAccountResponse Followee { get; set; }
}
