using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Domain.Identity;

namespace Woodshed.Infrastructure.Security;

public class UserAccessor(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor) : IUserAccessor
{
    public async Task<UserResponse?> GetCurrentUserAsync()
    {
        var claimsPrincipal = httpContextAccessor.HttpContext?.User;

        if (claimsPrincipal == null) return null;

        var user = await userManager.GetUserAsync(claimsPrincipal);

        if (user == null) return null;

        return new UserResponse
        {
            Id = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email,
            Name = user.Name,
            LastName = user.LastName,
            Biography = user.Biography,
            ImageUrl = user.ImageUrl,
            CreatedAt = user.CreatedAt,
        };
    }

    public string? GetUserIdOrDefault()
    {
        return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
