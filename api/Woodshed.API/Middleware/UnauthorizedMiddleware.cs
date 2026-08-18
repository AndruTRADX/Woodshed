using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.Middleware;

public class UnauthorizedMiddleware : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _default = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Challenged)
        {
            var problemDetails = new ApiResponse<object>(
                title: "Unauthorized",
                message: "You do not have access to this resource.",
                errors: []
            );

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
            return;
        }

        if (authorizeResult.Forbidden)
        {
            var problemDetails = new ApiResponse<object>(
                title: "Forbidden",
                message: "You do not have permission to perform this action.",
                errors: []
            );

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problemDetails);
            return;
        }

        await _default.HandleAsync(next, context, policy, authorizeResult);
    }
}
