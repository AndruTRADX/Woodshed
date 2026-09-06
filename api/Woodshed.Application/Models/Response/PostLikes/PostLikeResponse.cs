using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Models.Response.PostLikes;

public class PostLikeResponse
{
    public string PostId { get; set; } = string.Empty;
    public required UserAccountResponse User { get; set; }
}
