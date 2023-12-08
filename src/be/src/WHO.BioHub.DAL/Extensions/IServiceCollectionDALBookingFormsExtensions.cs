using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.BookingForms;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALBookingFormsExtensions
{
    public static IServiceCollection AddDALBookingForms(this IServiceCollection services)
    {
        services
            .AddScoped<IBookingFormReadRepository, SQLBookingFormReadRepository>()
            .AddScoped<IBookingFormWriteRepository, SQLBookingFormWriteRepository>();

        return services;
    }
}