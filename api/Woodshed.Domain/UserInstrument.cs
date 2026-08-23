using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;
using Woodshed.Domain.Identity;

namespace Woodshed.Domain;

[Table("tb_user_instrument")]
public class UserInstrument : BaseDomainModel
{
    [Column("user_id")]
    [MaxLength(36)]
    public string UserId { get; set; } = string.Empty;

    [Column("instrument_id")]
    [MaxLength(36)]
    public string InstrumentId { get; set; } = string.Empty;

    [Column("started_playing_at")]
    public required DateOnly StartedPlayingAt { get; set; }

    [Column("proficiency_id")]
    [MaxLength(36)]
    public string ProficiencyId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;
    public Instrument Instrument { get; set; } = null!;
    public InstrumentProficiency Proficiency { get; set; } = null!;
}
