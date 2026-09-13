namespace Woodshed.Application.Models.Request.Identity;

public class EditAccountRequest
{
    public string NickName { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? Biography { get; set; } = string.Empty;
}
