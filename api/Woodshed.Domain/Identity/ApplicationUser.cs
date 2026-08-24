using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;

namespace Woodshed.Domain.Identity;

public class ApplicationUser : IdentityUser
{
    [Column("nickname")]
    [MaxLength(64)]
    public string Nickname { get; set; } = string.Empty;

    [Column("name")]
    [MaxLength(155)]
    public string? Name { get; set; } = string.Empty;

    [Column("Last_name")]
    [MaxLength(155)]
    public string? LastName { get; set; } = string.Empty;

    [Column("biography")]
    [MaxLength(512)]
    public string? Biography { get; set; } = string.Empty;

    [Column("image_url")]
    [MaxLength(512)]
    public string? ImageUrl { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<Photo> Photos { get; set; } = [];
    public List<UserInstrument> UserInstruments { get; set; } = [];
    public List<UserFollower> Followers { get; set; } = [];
    public List<UserFollower> Following { get; set; } = [];
    public List<Post> Posts { get; set; } = [];
    public List<PostLike> PostLikes { get; set; } = [];
    public List<PostComment> PostComments { get; set; } = [];

    public Photo AddPhoto(string url, string publicId)
    {
        var photo = new Photo
        {
            Url = url,
            PublicId = publicId,
            UserId = Id,
        };

        Photos.Add(photo);
        ImageUrl ??= photo.Url;

        return photo;
    }

    public Photo RemovePhoto(string photoId)
    {
        var photo = Photos.Find(p => p.Id == photoId)
            ?? throw new DomainException("Photo not found for this user.");

        if (photo.Url == ImageUrl)
            throw new DomainException("Cannot delete main photo.");

        Photos.Remove(photo);

        return photo;
    }

    public Photo SetMainPhoto(string photoId)
    {
        var photo = Photos.Find(p => p.Id == photoId)
            ?? throw new DomainException("Photo not found for this user.");

        if (photo.Url != ImageUrl)
            ImageUrl = photo.Url;
        else
            throw new DomainException("This is already the main photo.");

        return photo;
    }

    public UserFollower Follow(ApplicationUser target)
    {
        if (target.Id == Id)
            throw new DomainException("You cannot follow yourself.");

        var alreadyFollowing = Following.Any(x => x.FolloweeId == target.Id);
        if (alreadyFollowing)
            throw new DomainException("You are already following this user.");

        var follow = new UserFollower
        {
            FollowerId = Id,
            FolloweeId = target.Id,
        };

        Following.Add(follow);

        return follow;
    }

    public void Unfollow(ApplicationUser target)
    {
        var follow = Following.Find(x => x.FolloweeId == target.Id)
            ?? throw new DomainException("You are not following this user.");

        Following.Remove(follow);
    }
}
