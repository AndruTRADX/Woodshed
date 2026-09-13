using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Woodshed.API.ExceptionHandlers;
using Woodshed.API.Filters;
using Woodshed.API.Middleware;

namespace Woodshed.API;

public static class ApiServiceRegistration
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<NoContentFilter>();
        });

        services.AddProblemDetails();
        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<BadRequestExceptionHandler>();
        services.AddExceptionHandler<UnauthorizedExceptionHandler>();
        services.AddExceptionHandler<ForbiddenExceptionHandler>();
        services.AddExceptionHandler<UnprocessableContentHandler>();
        services.AddExceptionHandler<DbUpdateExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddSingleton<IAuthorizationMiddlewareResultHandler, UnauthorizedMiddleware>();

        services.Configure<FormOptions>(opt =>
        {
            opt.MultipartBodyLengthLimit = 10 * 1024 * 1024;
        });

        return services;
    }
}