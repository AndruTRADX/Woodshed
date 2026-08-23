using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Woodshed.Domain.Common;
using Woodshed.Domain.Enums;

namespace Woodshed.Domain;

[Table("tb_instrument_proficiency")]
public class InstrumentProficiency : BaseDomainModel
{
    [Column("code", TypeName = "varchar(20)")]
    [Key]
    public required ProficiencyLevel Code { get; set; } 

    [Column("name")]
    [MaxLength(36)]
    public required string Name { get; set; }

    [Column("description")]
    [MaxLength(254)]
    public required string Description { get; set; }
}
