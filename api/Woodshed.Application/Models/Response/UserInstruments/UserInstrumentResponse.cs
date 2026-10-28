using Woodshed.Application.Models.Response.InstrumentProficiencies;
using Woodshed.Application.Models.Response.Instruments;

namespace Woodshed.Application.Models.Response.UserInstruments;

public record UserInstrumentResponse(
    string UserId, 
    string InstrumentId, 
    DateOnly StartedPlayingAt, 
    string ProficiencyId,
    InstrumentProficiencyResponse Proficiency,
    InstrumentResponse Instrument
);