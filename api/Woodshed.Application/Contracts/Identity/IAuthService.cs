using Woodshed.Application.Models.Request.Identity;
using Woodshed.Application.Models.Response.Identity;

namespace Woodshed.Application.Contracts.Identity;

public interface IAuthService
{
    Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request);
    Task SignOutAsync();
}
