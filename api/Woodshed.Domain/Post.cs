using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;
using Woodshed.Domain.Identity;

namespace Woodshed.Domain;

[Table("tb_post")]
public class Post : BaseDomainModel
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("content")]
    [MaxLength(3072)]
    public required string Content { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("user_id")]
    [MaxLength(36)]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public List<PostLike> Likes { get; set; } = [];
    public List<PostComment> Comments { get; set; } = [];

    public void Delete(string userId)
    {
        if (userId != UserId)
            throw new DomainException("You cannot delete others posts");
    }

    public void AddComment(PostComment comment, string userId)
    {
        comment.PostId = Id;
        comment.UserId = userId;

        Comments.Add(comment);
    }
}
