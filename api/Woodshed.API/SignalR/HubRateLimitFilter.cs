using System.Threading.RateLimiting;
using Microsoft.AspNetCore.SignalR;

namespace Woodshed.API.SignalR;

/// <summary>
/// The HTTP rate limiter only sees the request that opens a hub connection; every message after that
/// travels over the same WebSocket. This limits hub method calls per user, so nobody can flood a post
/// with comments through an open connection.
/// </summary>
public sealed class HubRateLimitFilter : IHubFilter, IDisposable
{
    // Bursts of up to 20 calls, refilling 10 every 10 seconds (about 1 call per second sustained).
    private readonly PartitionedRateLimiter<string> _limiter = PartitionedRateLimiter.Create<string, string>(key =>
        RateLimitPartition.GetTokenBucketLimiter(key, _ => new TokenBucketRateLimiterOptions
        {
            TokenLimit = 20,
            TokensPerPeriod = 10,
            ReplenishmentPeriod = TimeSpan.FromSeconds(10),
            QueueLimit = 0,
            AutoReplenishment = true,
        }));

    public async ValueTask<object?> InvokeMethodAsync(
        HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
    {
        var key = invocationContext.Context.UserIdentifier ?? invocationContext.Context.ConnectionId;

        using var lease = _limiter.AttemptAcquire(key);
        if (!lease.IsAcquired)
        {
            throw new HubException("You are sending too many requests. Please wait a moment and try again.");
        }

        return await next(invocationContext);
    }

    public void Dispose() => _limiter.Dispose();
}
