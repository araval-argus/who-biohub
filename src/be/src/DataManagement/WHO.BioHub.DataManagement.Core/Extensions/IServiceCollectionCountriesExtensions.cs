using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.CreateCountry;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.DeleteCountry;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.ListCountries;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.ReadCountry;
using WHO.BioHub.DataManagement.Core.UseCases.Countries.UpdateCountry;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionCountriesExtensions
{
    public static IServiceCollection AddCoreCountries(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCountryHandler, CreateCountryHandler>()
            .AddScoped<ICreateCountryMapper, CreateCountryMapper>()
            .AddScoped<CreateCountryCommandValidator>()

            .AddScoped<IReadCountryHandler, ReadCountryHandler>()
            .AddScoped<ReadCountryQueryValidator>()

            .AddScoped<IUpdateCountryHandler, UpdateCountryHandler>()
            .AddScoped<IUpdateCountryMapper, UpdateCountryMapper>()
            .AddScoped<UpdateCountryCommandValidator>()

            .AddScoped<IDeleteCountryHandler, DeleteCountryHandler>()
            .AddScoped<DeleteCountryCommandValidator>()

            .AddScoped<IListCountriesHandler, ListCountriesHandler>()
            .AddScoped<ListCountriesQueryValidator>()
            ;

        return services;
    }
}