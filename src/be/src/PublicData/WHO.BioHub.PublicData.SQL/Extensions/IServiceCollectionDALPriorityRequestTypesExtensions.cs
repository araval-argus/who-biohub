using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALPriorityRequestTypesExtensions
{
    public static IServiceCollection AddPublicDALPriorityRequestTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IPriorityRequestTypePublicReadRepository, SQLPriorityRequestTypePublicReadRepository>();

        return services;
    }
}