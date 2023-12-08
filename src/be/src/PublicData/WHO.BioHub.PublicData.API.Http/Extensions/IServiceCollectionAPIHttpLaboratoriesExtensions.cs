using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpLaboratoriesExtensions
{
    public static IServiceCollection AddAPIHttpLaboratories(this IServiceCollection services)
    {
        services
            .AddScoped<ILaboratoriesController, LaboratoriesController>();

        return services;
    }
}