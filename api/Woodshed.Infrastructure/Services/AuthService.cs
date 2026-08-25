using Microsoft.AspNetCore.Identity;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Request;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Domain.Identity;

namespace Woodshed.Infrastructure.Services;

public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
{
    public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest request)
    {
        var existingEmail = await userManager.FindByEmailAsync(request.Email);

        if (existingEmail is not null) throw new BadRequestException($"User with email \"{request.Email}\" already exists");

        var user = new ApplicationUser()
        {
            Email = request.Email,
            UserName = request.UserName,
            Name = request.Name,
            LastName = request.LastName,
            Biography = request.Biography,
            ImageUrl = request.ImageUrl,
        };

        var createResult = await userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            throw new BadRequestException(string.Join(", ", createResult.Errors.Select(e => e.Description)));
        }

        return new RegisterUserResponse
        {
            UserId = new Guid(user.Id)
        };
    }

    public async Task SignOutAsync()
    {
        await signInManager.SignOutAsync();
    }
}
