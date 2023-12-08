using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALInternationalTaxonomyClassificationsExtensions
{
    public static IServiceCollection AddDALInternationalTaxonomyClassifications(this IServiceCollection services)
    {
        services
            .AddScoped<IInternationalTaxonomyClassificationReadRepository, SQLInternationalTaxonomyClassificationReadRepository>()
            .AddScoped<IInternationalTaxonomyClassificationWriteRepository, SQLInternationalTaxonomyClassificationWriteRepository>();

        return services;
    }
}