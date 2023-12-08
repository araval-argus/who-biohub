using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALResourcesExtensions
{
    public static IServiceCollection AddDALResources(this IServiceCollection services)
    {
        services
            .AddScoped<IResourceReadRepository, SQLResourceReadRepository>()
            .AddScoped<IResourceWriteRepository, SQLResourceWriteRepository>();

        return services;
    }
}