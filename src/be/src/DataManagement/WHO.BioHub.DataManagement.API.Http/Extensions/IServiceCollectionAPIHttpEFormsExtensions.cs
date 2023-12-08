using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpEFormsExtensions
{
    public static IServiceCollection AddAPIHttpEForms(this IServiceCollection services)
    {
        services
            .AddScoped<IEFormsController, EFormsController>();

        return services;
    }
}