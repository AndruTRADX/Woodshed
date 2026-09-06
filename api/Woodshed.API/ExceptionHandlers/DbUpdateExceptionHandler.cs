using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Woodshed.Application.Models.Response.Common;

namespace Woodshed.API.ExceptionHandlers;

public class DbUpdateExceptionHandler(ILogger<DbUpdateExceptionHandler> logger) : IExceptionHandler
{
    private static readonly int[] UniqueConstraintErrorNumbers = [2627, 2601];

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not DbUpdateException dbUpdateException)
        {
            return false;
        }

        if (dbUpdateException.InnerException is not SqlException sqlException
            || !UniqueConstraintErrorNumbers.Contains(sqlException.Number))
        {
            return false;
        }

        logger.LogWarning(dbUpdateException, "Duplicate key violation: {Message}", sqlException.Message);

        var problemDetails = new ApiResponse<object>("Conflict", "This record already exists.", []);

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}