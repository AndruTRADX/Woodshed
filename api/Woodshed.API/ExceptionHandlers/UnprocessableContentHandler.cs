using Microsoft.AspNetCore.Diagnostics;
using Woodshed.Application.Models.Response.Common;
using Woodshed.Domain.Common;

namespace Woodshed.API.ExceptionHandlers;

public class UnprocessableContentHandler(ILogger<UnprocessableContentHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not DomainException unprocessableException)
        {
            return false;
        }

        logger.LogWarning(unprocessableException, "Unprocessable Content access attempt: {Message}", unprocessableException.Message);

        var problemDetails = new ApiResponse<object>("Unprocessable Content", unprocessableException.Message, []);

        httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
