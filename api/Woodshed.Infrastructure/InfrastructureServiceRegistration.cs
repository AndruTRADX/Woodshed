using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Woodshed.Application.Contracts.Identity;
using Woodshed.Application.Contracts.Persistence;
using Woodshed.Application.Contracts.Photos;
using Woodshed.Infrastructure.Models;
using Woodshed.Infrastructure.Persistence;
using Woodshed.Infrastructure.Repositories;
using Woodshed.Infrastructure.Security;
using Woodshed.Infrastructure.Services;

namespace Woodshed.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });


        services.AddHttpContextAccessor();

        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Photos
        services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
        services.AddScoped<IPhotoService, PhotoService>();

        // Identity
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserProfileService, UserProfileService>();
        services.AddScoped<IUserAccessor, UserAccessor>();

        return services;
    }
}
