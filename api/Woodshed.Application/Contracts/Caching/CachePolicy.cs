namespace Woodshed.Application.Contracts.Caching;

/// <summary>
/// Expiration for a cache entry. Sliding resets on every read; absolute is the hard upper bound
/// (a sliding-only entry that is read often enough would never expire).
/// </summary>
public record CachePolicy(TimeSpan? Sliding = null, TimeSpan? Absolute = null);
