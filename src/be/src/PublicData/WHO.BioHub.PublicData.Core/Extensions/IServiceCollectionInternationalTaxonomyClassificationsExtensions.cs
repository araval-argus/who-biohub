using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.InternationalTaxonomyClassifications.ListInternationalTaxonomyClassifications;
using WHO.BioHub.PublicData.Core.UseCases.InternationalTaxonomyClassifications.ReadInternationalTaxonomyClassification;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionInternationalTaxonomyClassificationsExtensions
{
    public static IServiceCollection AddCoreInternationalTaxonomyClassifications(this IServiceCollection services)
    {
        services
            .AddScoped<IReadInternationalTaxonomyClassificationHandler, ReadInternationalTaxonomyClassificationHandler>()
            .AddScoped<ReadInternationalTaxonomyClassificationQueryValidator>()

            .AddScoped<IListInternationalTaxonomyClassificationsHandler, ListInternationalTaxonomyClassificationsHandler>()
            .AddScoped<ListInternationalTaxonomyClassificationsQueryValidator>()
            ;

        return services;
    }
}