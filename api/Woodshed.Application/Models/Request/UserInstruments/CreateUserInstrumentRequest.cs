using Woodshed.Domain.Enums;

namespace Woodshed.Application.Models.Request.UserInstruments;

public class CreateUserInstrumentRequest
{
    public string InstrumentId { get; set; } = string.Empty;
    public ProficiencyLevel ProficiencyId { get; set; }
    public required DateOnly StartedPlayingAt { get; set; }
}
