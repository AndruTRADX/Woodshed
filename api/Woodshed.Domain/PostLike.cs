using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;
using Woodshed.Domain.Identity;

namespace Woodshed.Domain;

[Table("tb_post_like")]
public class PostLike : BaseDomainModel
{
    [Column("post_id")]
    [MaxLength(36)]
    public string PostId { get; set; } = string.Empty;

    [Column("user_id")]
    [MaxLength(36)]
    public string UserId { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Post Post { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}
