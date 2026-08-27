namespace Woodshed.Application.Models.Response.Posts;

public class PostResponse
{
    public required string Id { get; set; }
    public required string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public required string UserId { get; set; }
}
