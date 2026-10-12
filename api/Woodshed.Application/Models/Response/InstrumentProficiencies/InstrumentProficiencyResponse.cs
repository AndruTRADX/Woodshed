using Woodshed.Domain.Enums;

namespace Woodshed.Application.Models.Response.InstrumentProficiencies;

public record InstrumentProficiencyResponse(ProficiencyLevel Code, string Name, string Description);