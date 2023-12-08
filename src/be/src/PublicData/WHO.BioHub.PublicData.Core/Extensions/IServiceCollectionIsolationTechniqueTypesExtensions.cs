using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;
using WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ReadIsolationTechniqueType;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionIsolationTechniqueTypesExtensions
{
    public static IServiceCollection AddCoreIsolationTechniqueTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IReadIsolationTechniqueTypeHandler, ReadIsolationTechniqueTypeHandler>()
            .AddScoped<ReadIsolationTechniqueTypeQueryValidator>()

            .AddScoped<IListIsolationTechniqueTypesHandler, ListIsolationTechniqueTypesHandler>()
            .AddScoped<ListIsolationTechniqueTypesQueryValidator>()
            ;

        return services;
    }
}