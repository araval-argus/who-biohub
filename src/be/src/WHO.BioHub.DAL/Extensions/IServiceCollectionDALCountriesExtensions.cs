using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Countries;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALCountriesExtensions
{
    public static IServiceCollection AddDALCountries(this IServiceCollection services)
    {
        services
            .AddScoped<ICountryReadRepository, SQLCountryReadRepository>()
            .AddScoped<ICountryWriteRepository, SQLCountryWriteRepository>();

        return services;
    }
}