using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;
using Woodshed.Domain.Identity;

namespace Woodshed.Domain;

[Table("tb_user_follower")]
public class UserFollower : BaseDomainModel
{
    [Column("follower_id")]
    [MaxLength(36)]
    public string FollowerId { get; set; } = string.Empty;

    [Column("followee_id")]
    [MaxLength(36)]
    public string FolloweeId { get; set; } = string.Empty;

    public ApplicationUser Follower { get; set; } = null!; // Observer
    public ApplicationUser Followee { get; set; } = null!; // Target
}
