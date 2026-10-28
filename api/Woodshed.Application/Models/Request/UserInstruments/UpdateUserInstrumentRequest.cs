using Woodshed.Domain.Enums;

namespace Woodshed.Application.Models.Request.UserInstruments;

public class UpdateUserInstrumentRequest
{
    public ProficiencyLevel ProficiencyId { get; set; }
    public required DateOnly StartedPlayingAt { get; set; }
}
