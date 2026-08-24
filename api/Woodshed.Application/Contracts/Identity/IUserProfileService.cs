using Woodshed.Domain;

namespace Woodshed.Application.Contracts.Identity;

public interface IUserProfileService
{
    Task<Photo> AddPhotoAsync(string userId, string url, string publicId, CancellationToken cancellationToken);
    Task<Photo> RemovePhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task SetMainPhotoAsync(string userId, string photoId, CancellationToken cancellationToken);
    Task FollowAsync(string userId, string targetUserId, CancellationToken cancellationToken);
    Task UnfollowAsync(string userId, string targetUserId, CancellationToken cancellationToken);
}
