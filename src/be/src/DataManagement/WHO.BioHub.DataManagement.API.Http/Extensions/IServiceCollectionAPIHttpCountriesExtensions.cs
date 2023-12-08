using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpCountriesExtensions
{
    public static IServiceCollection AddAPIHttpCountries(this IServiceCollection services)
    {
        services
            .AddScoped<ICountriesController, CountriesController>();

        return services;
    }
}