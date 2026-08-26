namespace Woodshed.Application.Models.Response.Identity;

public class UserResponse
{
    public string Id { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Biography { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
