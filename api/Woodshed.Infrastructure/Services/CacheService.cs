using System.Collections.Concurrent;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Woodshed.Application.Contracts.Caching;

namespace Woodshed.Infrastructure.Services;

// Singleton: it owns the locks and the "clear" token, so it must outlive a request.
public sealed class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private static readonly CachePolicy DefaultPolicy = new(
        Sliding: TimeSpan.FromMinutes(10),
        Absolute: TimeSpan.FromHours(5));

    // GetOrCreate is not atomic: two requests that miss at the same time would both hit the database.
    // A lock per key stripe makes the second one wait and then find the value the first one cached.
    // Fixed size on purpose: it stays bounded no matter how many distinct keys show up.
    private readonly SemaphoreSlim[] _locks = [.. Enumerable
        .Range(0, 32)
        .Select(_ => new SemaphoreSlim(1, 1))];

    // Every entry hangs off this token. Cancelling it evicts all of them at once, without
    // touching entries that other parts of the app (or the framework) put in the shared IMemoryCache.
    // Not disposed on purpose: a Clear racing with Set must not make `.Token` throw.
    private CancellationTokenSource _clearSource = new();

    // One token source per group (the pattern from the docs: "evict multiple cache entries as a group").
    // Group names are defined in code (CacheKeys), never from user input, so this stays small.
    private readonly ConcurrentDictionary<string, CancellationTokenSource> _groups = new();

    public bool TryGet<T>(string key, out T? value) where T : class
        => memoryCache.TryGetValue(key, out value);

    public async Task<T?> GetOrCreateAsync<T>(
        string key,
        Func<CancellationToken, Task<T?>> factory,
        CachePolicy? policy = null,
        string? group = null,
        CancellationToken cancellationToken = default) where T : class
    {
        if (memoryCache.TryGetValue(key, out T? cached))
        {
            return cached;
        }

        var gate = _locks[(uint)key.GetHashCode() % (uint)_locks.Length];
        await gate.WaitAsync(cancellationToken);

        try
        {
            if (memoryCache.TryGetValue(key, out cached))
            {
                return cached;
            }

            var value = await factory(cancellationToken);

            if (value is not null)
            {
                Set(key, value, policy, group);
            }

            return value;
        }
        finally
        {
            gate.Release();
        }
    }

    public void Set<T>(string key, T value, CachePolicy? policy = null, string? group = null) where T : class
    {
        policy ??= DefaultPolicy;

        var options = new MemoryCacheEntryOptions()
            .AddExpirationToken(new CancellationChangeToken(Volatile.Read(ref _clearSource).Token));

        if (group is not null)
        {
            // If RemoveGroup wins the race, we get an already-cancelled token and the entry is
            // evicted right away: stale data is dropped instead of surviving the invalidation.
            var groupSource = _groups.GetOrAdd(group, _ => new CancellationTokenSource());
            options.AddExpirationToken(new CancellationChangeToken(groupSource.Token));
        }

        if (policy.Sliding is { } sliding)
        {
            options.SetSlidingExpiration(sliding);
        }

        if (policy.Absolute is { } absolute)
        {
            options.SetAbsoluteExpiration(absolute);
        }

        memoryCache.Set(key, value, options);
    }

    public void Remove(string key) => memoryCache.Remove(key);

    public void RemoveGroup(string group)
    {
        if (_groups.TryRemove(group, out var groupSource))
        {
            groupSource.Cancel();
        }
    }

    public void Clear()
    {
        var previous = Interlocked.Exchange(ref _clearSource, new CancellationTokenSource());
        previous.Cancel();
    }
}
