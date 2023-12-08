using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.BookingFormsHistory;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALBookingFormsHistoryExtensions
{
    public static IServiceCollection AddDALBookingFormsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<IBookingFormHistoryReadRepository, SQLBookingFormHistoryReadRepository>()
            .AddScoped<IBookingFormHistoryWriteRepository, SQLBookingFormHistoryWriteRepository>();

        return services;
    }
}