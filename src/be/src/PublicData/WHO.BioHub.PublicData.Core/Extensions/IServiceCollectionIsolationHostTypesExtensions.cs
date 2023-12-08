using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;
using WHO.BioHub.PublicData.Core.UseCases.IsolationHostTypes.ReadIsolationHostType;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionIsolationHostTypesExtensions
{
    public static IServiceCollection AddCoreIsolationHostTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IReadIsolationHostTypeHandler, ReadIsolationHostTypeHandler>()
            .AddScoped<ReadIsolationHostTypeQueryValidator>()

            .AddScoped<IListIsolationHostTypesHandler, ListIsolationHostTypesHandler>()
            .AddScoped<ListIsolationHostTypesQueryValidator>()
            ;

        return services;
    }
}