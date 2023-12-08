using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.BSLLevels.ListBSLLevels;
using WHO.BioHub.PublicData.Core.UseCases.BSLLevels.ReadBSLLevel;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionBSLLevelsExtensions
{
    public static IServiceCollection AddCoreBSLLevels(this IServiceCollection services)
    {
        services
            .AddScoped<IReadBSLLevelHandler, ReadBSLLevelHandler>()
            .AddScoped<ReadBSLLevelQueryValidator>()

            .AddScoped<IListBSLLevelsHandler, ListBSLLevelsHandler>()
            .AddScoped<ListBSLLevelsQueryValidator>()
            ;

        return services;
    }
}