using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALInternationalTaxonomyClassificationsExtensions
{
    public static IServiceCollection AddPublicDALInternationalTaxonomyClassifications(this IServiceCollection services)
    {
        services
            .AddScoped<IInternationalTaxonomyClassificationPublicReadRepository, SQLInternationalTaxonomyClassificationPublicReadRepository>();

        return services;
    }
}