using System.Globalization;
using System.Security.Claims;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.Security;

public static class RateLimitPolicies
{
    /// <summary>Login and register: strict and per IP, to slow down brute force and mass sign-ups.</summary>
    public const string Auth = "auth";

    /// <summary>File uploads: decoding images is expensive, so they get their own budget.</summary>
    public const string Uploads = "uploads";
}

public static class RateLimitingExtensions
{
    public static IServiceCollection AddApiRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

            options.OnRejected = async (context, cancellationToken) =>
            {
                if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                {
                    context.HttpContext.Response.Headers.RetryAfter =
                        ((int)Math.Ceiling(retryAfter.TotalSeconds)).ToString(CultureInfo.InvariantCulture);
                }

                var response = new ApiResponse<object>(
                    "Too Many Requests",
                    "You are sending too many requests. Please wait a moment and try again.",
                    []);
                await context.HttpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            };

            // Every API and hub request, per user (or per IP when anonymous). Static files of the SPA
            // are left out: a single page load pulls many of them.
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
            {
                var path = context.Request.Path;
                if (!path.StartsWithSegments("/api") && !path.StartsWithSegments("/hubs"))
                {
                    return RateLimitPartition.GetNoLimiter("static");
                }

                return RateLimitPartition.GetFixedWindowLimiter(UserOrIpKey(context), _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 300,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0,
                });
            });

            options.AddPolicy(RateLimitPolicies.Auth, context =>
                RateLimitPartition.GetFixedWindowLimiter(IpKey(context), _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0,
                }));

            options.AddPolicy(RateLimitPolicies.Uploads, context =>
                RateLimitPartition.GetFixedWindowLimiter(UserOrIpKey(context), _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0,
                }));
        });

        return services;
    }

    private static string UserOrIpKey(HttpContext context)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return string.IsNullOrEmpty(userId) ? IpKey(context) : $"user:{userId}";
    }

    // Behind a reverse proxy this is only the real client IP if UseForwardedHeaders trusts that proxy.
    private static string IpKey(HttpContext context) =>
        $"ip:{context.Connection.RemoteIpAddress?.ToString() ?? "unknown"}";
}
