using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;

namespace Woodshed.Domain;

[Table("tb_instrument")]
public class Instrument : BaseDomainModel
{
    [Column("id")]
    [Key]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("name")]
    [MaxLength(155)]
    public required string  Name { get; set; }

    public List<UserInstrument> UserInstruments { get; set; }  = [];
}
