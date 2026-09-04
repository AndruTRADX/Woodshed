using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Models.Response.PostComments;

public class PostCommentResponse
{
    public required string Id { get; set; }
    public required string Content { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required string UserId { get; set; }
    public required UserAccountResponse User { get; set; }
}
