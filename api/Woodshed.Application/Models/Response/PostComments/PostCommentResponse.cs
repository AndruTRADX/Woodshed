using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Models.Response.PostComments;

public class PostCommentResponse
{
    public required string Id { get; set; }
    public required string PostId { get; set; }
    public required string Content { get; set; }
    public bool HasBeenEdited { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime? EditedAt { get; set; }
    public required UserAccountResponse User { get; set; }
}
