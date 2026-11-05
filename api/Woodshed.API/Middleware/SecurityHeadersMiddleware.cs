namespace Woodshed.API.Middleware;

public static class SecurityHeadersExtensions
{
    // The SPA only loads its own bundle; the one external origin is Cloudinary for photos.
    // 'unsafe-inline' for styles is needed by the UI components, which set inline style attributes.
    // connect-src 'self' also covers the SignalR WebSocket on the same origin.
    private const string ContentSecurityPolicy =
        "default-src 'self'; " +
        "script-src 'self'; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data: blob: https://res.cloudinary.com; " +
        "font-src 'self' data:; " +
        "media-src 'self'; " +
        "connect-src 'self'; " +
        "object-src 'none'; " +
        "base-uri 'self'; " +
        "form-action 'self'; " +
        "frame-ancestors 'none'";

    /// <summary>
    /// Adds browser security headers to every response. The Content-Security-Policy is skipped in
    /// development, where the front end is served by Vite and Swagger UI relies on inline scripts.
    /// </summary>
    public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, bool isDevelopment)
    {
        return app.Use(async (context, next) =>
        {
            var headers = context.Response.Headers;

            headers.XContentTypeOptions = "nosniff";
            headers.XFrameOptions = "DENY";
            headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
            headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=(), payment=()";

            if (!isDevelopment)
            {
                headers.ContentSecurityPolicy = ContentSecurityPolicy;
            }

            await next(context);
        });
    }
}
