namespace Woodshed.Application.Contracts.Caching;

/// <summary>
/// Single place where cache keys are defined, so a query and the commands that invalidate it
/// always agree on the key. Keys come from here, never straight from user input.
/// </summary>
public static class CacheKeys
{
    // Instruments
    public const string Instruments = "instruments";
    public const string InstrumentsGroup = "instruments";
    public static string Instrument(string id) => $"instruments:{id}";

    public const string InstrumentProficiency = "InstrumentProficiency";
    public const string InstrumentProficiencyGroup = "InstrumentProficiency";

    public static string UserInstrument(string userId) => $"instrumentsUser:{userId}";
    public const string UserInstrumentGroup = "instrumentsUser";
    public static string UserInstrument(string userId, string instrumentId) => $"instrumentsUser:{userId}:{instrumentId}";
    
}
