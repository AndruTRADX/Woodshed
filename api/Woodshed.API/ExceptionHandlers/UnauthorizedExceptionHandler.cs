using Microsoft.AspNetCore.Diagnostics;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.ExceptionHandlers;

public class UnauthorizedExceptionHandler(ILogger<UnauthorizedExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not UnauthorizedException unauthorizedException)
        {
            return false;
        }

        logger.LogWarning(unauthorizedException, "Unauthorized access attempt: {Message}", unauthorizedException.Message);

        var problemDetails = new ApiResponse<object>("Unauthorized", unauthorizedException.Message, []);

        httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
