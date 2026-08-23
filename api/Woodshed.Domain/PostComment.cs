using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;
using Woodshed.Domain.Identity;

namespace Woodshed.Domain;

[Table("tb_post_comment")]
public class PostComment : BaseDomainModel
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("content")]
    [MaxLength(512)]
    public required string Content { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("user_id")]
    [MaxLength(36)]
    public string UserId { get; set; } = string.Empty;

    [Column("post_id")]
    [MaxLength(36)]
    public string PostId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;
    public Post Post { get; set; } = null!;
}
