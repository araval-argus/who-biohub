using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpInternationalTaxonomyClassificationsExtensions
{
    public static IServiceCollection AddAPIHttpInternationalTaxonomyClassifications(this IServiceCollection services)
    {
        services
            .AddScoped<IInternationalTaxonomyClassificationsController, InternationalTaxonomyClassificationsController>();

        return services;
    }
}
