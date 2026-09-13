using Woodshed.Application.Models.Response.Identity;
using Woodshed.Domain;

namespace Woodshed.Application.Contracts.Identity;

public interface IUserAccountService
{
    Task<UserAccountResponse> GetUserAccount(string userId, CancellationToken cancellationToken);
    Task<UserAccountResponse> EditAccountAsync(
        string userId, 
        string nickName, string? name, string? lastName, string? biography,
        CancellationToken cancellationToken);
    Task<Photo> AddPhotoAsync(string userId, string url, string publicId, CancellationToken cancellationToken);
    Task<Photo> RemovePhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task SetMainPhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task FollowAsync(string userId, string targetUserId, CancellationToken cancellationToken);
    Task UnfollowAsync(string userId, string targetUserId, CancellationToken cancellationToken);
    
}
