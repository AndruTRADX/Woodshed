using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Contracts.Identity;

public interface IUserAccessor
{
    Task<UserResponse?> GetCurrentUserAsync();
    public string? GetUserIdOrDefault();
}
