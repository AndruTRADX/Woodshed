namespace Woodshed.Application.Models.Request.Identity;

public class RegisterUserRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string NickName { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Biography { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
}
