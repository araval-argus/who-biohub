using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALUserRequestsExtensions
{
    public static IServiceCollection AddDALUserRequests(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestReadRepository, SQLUserRequestReadRepository>()
            .AddScoped<IUserRequestWriteRepository, SQLUserRequestWriteRepository>();

        return services;
    }
}