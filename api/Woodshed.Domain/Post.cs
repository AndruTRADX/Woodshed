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

    public List<PostLike> PostLikes { get; set; } = [];
    public List<PostComment> PostComments { get; set; } = [];
}
