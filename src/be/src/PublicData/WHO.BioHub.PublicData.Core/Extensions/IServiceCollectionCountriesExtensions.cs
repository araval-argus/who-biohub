using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.Countries.ListCountries;
using WHO.BioHub.PublicData.Core.UseCases.Countries.ReadCountry;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionCountriesExtensions
{
    public static IServiceCollection AddCoreCountries(this IServiceCollection services)
    {
        services
            .AddScoped<IReadCountryHandler, ReadCountryHandler>()
            .AddScoped<ReadCountryQueryValidator>()

            .AddScoped<IListCountriesHandler, ListCountriesHandler>()
            .AddScoped<ListCountriesQueryValidator>()
            ;

        return services;
    }
}