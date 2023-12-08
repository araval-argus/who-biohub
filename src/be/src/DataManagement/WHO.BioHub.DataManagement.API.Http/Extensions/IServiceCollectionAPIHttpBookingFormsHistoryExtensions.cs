using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpBookingFormsHistoryExtensions
{
    public static IServiceCollection AddAPIHttpBookingFormsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<IBookingFormsHistoryController, BookingFormsHistoryController>();

        return services;
    }
}