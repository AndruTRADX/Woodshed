using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Domain;
using Woodshed.Domain.Identity;

namespace Woodshed.Infrastructure.Services;

public class UserProfileService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork) : IUserProfileService
{
    public async Task<Photo> AddPhotoAsync(string userId, string url, string publicId, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new UnauthorizedException();

        var photo = user.AddPhoto(url, publicId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return photo;
    }

    public async Task<Photo> RemovePhotoAsync(string userId, string photoId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var photo = user.RemovePhoto(photoId);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return photo;
    }

    public async Task SetMainPhotoAsync(string userId, string photoId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Photos).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var photo = user.SetMainPhoto(photoId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task FollowAsync(string userId, string targetUserId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Following).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var target = await userManager.FindByIdAsync(targetUserId)
            ?? throw new NotFoundException("UserProfile", targetUserId);

        user.Follow(target);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnfollowAsync(string userId, string targetUserId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Following).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var target = await userManager.FindByIdAsync(targetUserId)
            ?? throw new NotFoundException("UserProfile", targetUserId);

        user.Unfollow(target);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}