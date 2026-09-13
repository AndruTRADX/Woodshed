using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Identity;
using Woodshed.Domain;
using Woodshed.Domain.Identity;

namespace Woodshed.Infrastructure.Services;

public class UserAccountService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IMapper mapper) : IUserAccountService
{
    public async Task<UserAccountResponse> GetUserAccount(string userId, CancellationToken cancellationToken)
    {
        return await userManager.Users
            .Where(u => u.Id == userId)
            .ProjectTo<UserAccountResponse>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("UserAccount", userId);
    }

    public async Task<UserAccountResponse> EditAccountAsync(
    string userId,
    string nickName, string? name, string? lastName, string? biography,
    CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(userId)
            ?? throw new UnauthorizedException();

        var nickNameChanged = !string.Equals(user.NickName, nickName, StringComparison.Ordinal);

        if (nickNameChanged)
        {
            var nickNameTaken = await userManager.Users
                .AnyAsync(u => u.NickName == nickName && u.Id != userId, cancellationToken);

            if (nickNameTaken)
                throw new BadRequestException($"Nickname \"{nickName}\" is already taken");
        }

        user.EditAccount(nickName, name, lastName, biography);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserAccountResponse>(user);
    }

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
            ?? throw new NotFoundException("UserAccount", targetUserId);

        user.Follow(target);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UnfollowAsync(string userId, string targetUserId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users.Include(u => u.Following).FirstOrDefaultAsync(x => x.Id == userId, cancellationToken: cancellationToken)
            ?? throw new UnauthorizedException();

        var target = await userManager.FindByIdAsync(targetUserId)
            ?? throw new NotFoundException("UserAccount", targetUserId);

        user.Unfollow(target);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}