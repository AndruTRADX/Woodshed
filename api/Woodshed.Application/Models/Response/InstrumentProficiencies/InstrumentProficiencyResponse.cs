using Woodshed.Domain.Enums;

namespace Woodshed.Application.Models.Response.InstrumentProficiencies;

public record InstrumentProficiencyResponse(string Code, string Name, string Description);