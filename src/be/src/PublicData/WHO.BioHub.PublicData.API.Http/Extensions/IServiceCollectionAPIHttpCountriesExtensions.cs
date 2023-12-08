using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpCountriesExtensions
{
    public static IServiceCollection AddAPIHttpCountries(this IServiceCollection services)
    {
        services
            .AddScoped<ICountriesController, CountriesController>();

        return services;
    }
}