using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Search.Core.Repositories.Aggregates;
using WHO.BioHub.Search.SQL.Repositories.Aggregates;

namespace WHO.BioHub.Search.SQL.Extensions;

public static class IServiceCollectionSQLSearchFrontEndGlobalSearchExtensions
{
    public static IServiceCollection AddSQLSearchFrontEndGlobalSearch(this IServiceCollection services)
    {
        services
            .AddScoped<IFrontEndGlobalSearchRepository, SQLFrontEndGlobalSearchRepository>();

        return services;
    }
}