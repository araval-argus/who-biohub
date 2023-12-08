using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpSpecimenTypesExtensions
{
    public static IServiceCollection AddAPIHttpSpecimenTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ISpecimenTypesController, SpecimenTypesController>();

        return services;
    }
}