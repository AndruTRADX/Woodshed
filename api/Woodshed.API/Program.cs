using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Woodshed.API;
using Woodshed.API.Middleware;
using Woodshed.API.Security;
using Woodshed.API.SignalR;
using Woodshed.Application;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain.Identity;
using Woodshed.Infrastructure;
using Woodshed.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HubRateLimitFilter>();
builder.Services.AddSignalR(options => options.AddFilter<HubRateLimitFilter>());
builder.Services.AddApiRateLimiting();

// Behind a reverse proxy (nginx, Caddy...) this restores the real client IP and scheme.
// By default only a proxy on the same machine (loopback) is trusted; add others to KnownProxies.
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

// Services
builder.Services.AddApiServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Identity
builder.Services.AddIdentityApiEndpoints<ApplicationUser>(opt =>
{
    opt.User.RequireUniqueEmail = true;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    // Lax keeps the cookie off cross-site requests, which is what protects the API and the
    // SignalR WebSocket from CSRF. Do not change it to None. Strict also works, if ever needed
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = builder.Environment.IsDevelopment()
        ? CookieSecurePolicy.SameAsRequest
        : CookieSecurePolicy.Always;
});

// We should not have this in production though: change when deploying
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
    ?? (builder.Environment.IsDevelopment() ? ["http://localhost:5173", "https://localhost:5173"] : []);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins(allowedOrigins));
});

var app = builder.Build();

app.UseForwardedHeaders();

app.UseExceptionHandler();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseSecurityHeaders(app.Environment.IsDevelopment());

app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGroup("api")
    .MapIdentityApi<ApplicationUser>()
    .AddEndpointFilter(async (context, next) =>
        context.HttpContext.Request.Path.StartsWithSegments("/api/register")
            ? Results.NotFound()
            : await next(context))
    .RequireRateLimiting(RateLimitPolicies.Auth);

app.MapControllers();

/*** 
    TODO: Find a better place to leave these MapHubs, in an independent file
    where we can place multiple of them
***/
app.MapHub<PostCommentHub>("/hubs/comments"); 

// Unknown API or hub routes get a JSON 404 instead of the SPA's index.html.
var notFoundEndpoint = () => Results.NotFound(
    new ApiResponse<object>("Resource Not Found", "This endpoint does not exist.", []));
app.MapFallback("api/{**path}", notFoundEndpoint);
app.MapFallback("hubs/{**path}", notFoundEndpoint);

app.MapFallbackToController("Index", "Fallback");

using (var scoped = app.Services.CreateScope())
{
    var service = scoped.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();

        await AppDbContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Migration error");
        throw;
    }
}

app.Run();
