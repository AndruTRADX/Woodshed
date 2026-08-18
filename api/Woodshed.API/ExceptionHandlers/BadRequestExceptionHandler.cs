using Microsoft.AspNetCore.Diagnostics;
using Woodshed.Application.Exceptions;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.ExceptionHandlers;

public class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not BadRequestException badRequestException)
        {
            return false;
        }

        logger.LogWarning(badRequestException, "Bad request: {Message}", badRequestException.Message);

        var problemDetails = new ApiResponse<object>("Bad Request", badRequestException.Message, []);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
