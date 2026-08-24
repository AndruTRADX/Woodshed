using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;
using Woodshed.Domain.Identity;

namespace Woodshed.Domain;

[Table("tb_photo")]
public class Photo : BaseDomainModel
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("url")]
    [MaxLength(512)]
    public required string Url { get; set; }

    [Column("public_id")]
    [MaxLength(255)]
    public string PublicId { get; set; } = string.Empty;

    [Column("user_id")]
    [MaxLength(36)]
    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;
}
