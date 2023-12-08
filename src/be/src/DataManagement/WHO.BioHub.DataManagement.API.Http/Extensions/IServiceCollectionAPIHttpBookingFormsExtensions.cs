using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpBookingFormsExtensions
{
    public static IServiceCollection AddAPIHttpBookingForms(this IServiceCollection services)
    {
        services
            .AddScoped<IBookingFormsController, BookingFormsController>();

        return services;
    }
}