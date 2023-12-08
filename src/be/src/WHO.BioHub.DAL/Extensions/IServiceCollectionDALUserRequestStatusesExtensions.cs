using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.UserRequestStatuses;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALUserRequestStatusesExtensions
{
    public static IServiceCollection AddDALUserRequestStatuses(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestStatusReadRepository, SQLUserRequestStatusReadRepository>()
            .AddScoped<IUserRequestStatusWriteRepository, SQLUserRequestStatusWriteRepository>();

        return services;
    }
}