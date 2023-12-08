using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALCountriesExtensions
{
    public static IServiceCollection AddPublicDALCountries(this IServiceCollection services)
    {
        services
            .AddScoped<ICountryPublicReadRepository, SQLCountryPublicReadRepository>();

        return services;
    }
}