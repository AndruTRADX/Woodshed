using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    public List<UserInstrument> UserInstruments { get; set; }  = [];
    public List<UserFollower> Followers { get; set; } = [];
    public List<UserFollower> Following { get; set; } = [];
    public List<Post> Posts { get; set; } = [];
    public List<PostLike> PostLikes { get; set; } = [];
    public List<PostComment> PostComments { get; set; } = [];
}
