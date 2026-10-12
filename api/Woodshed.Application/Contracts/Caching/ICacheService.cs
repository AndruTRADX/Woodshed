namespace Woodshed.Application.Contracts.Caching;

public interface ICacheService
{
    /// <summary>Returns the cached value, or false if the key is not in the cache.</summary>
    bool TryGet<T>(string key, out T? value) where T : class;

    /// <summary>
    /// Returns the cached value; on a miss runs <paramref name="factory"/> (typically the database call)
    /// exactly once, even with concurrent callers, and caches its result. A null result is not cached.
    /// Do not call this from inside a <paramref name="factory"/>.
    /// </summary>
    /// <param name="group">Optional group the entry belongs to, so <see cref="RemoveGroup"/> can evict it with the rest.</param>
    Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T?>> factory,
        CachePolicy? policy = null,
        string? group = null,
        CancellationToken cancellationToken = default) where T : class;

    /// <summary>Adds or replaces the entry for <paramref name="key"/>.</summary>
    /// <param name="group">Optional group the entry belongs to, so <see cref="RemoveGroup"/> can evict it with the rest.</param>
    void Set<T>(string key, T value, CachePolicy? policy = null, string? group = null) where T : class;

    /// <summary>Removes a single entry. Does not touch any other entry, even related ones.</summary>
    void Remove(string key);

    /// <summary>Removes every entry that was stored with this <paramref name="group"/>.</summary>
    void RemoveGroup(string group);

    /// <summary>Removes every entry created through this service (and only those).</summary>
    void Clear();
}
