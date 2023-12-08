using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.Core.UseCases.Resources.ListResources;
using WHO.BioHub.PublicData.Core.UseCases.Resources.ReadResourceFileToken;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionResourcesExtensions
{
    public static IServiceCollection AddCoreResources(this IServiceCollection services)
    {
        services
            .AddScoped<IReadResourceFileTokenHandler, ReadResourceFileTokenHandler>()
            .AddScoped<ReadResourceFileTokenQueryValidator>()

            .AddScoped<IListResourcesHandler, ListResourcesHandler>()
            .AddScoped<ListResourcesQueryValidator>()
            ;

        return services;
    }
}