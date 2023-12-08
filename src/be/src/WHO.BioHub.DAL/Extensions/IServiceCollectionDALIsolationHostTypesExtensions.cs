using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.IsolationHostTypes;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALIsolationHostTypesExtensions
{
    public static IServiceCollection AddDALIsolationHostTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IIsolationHostTypeReadRepository, SQLIsolationHostTypeReadRepository>()
            .AddScoped<IIsolationHostTypeWriteRepository, SQLIsolationHostTypeWriteRepository>();

        return services;
    }
}