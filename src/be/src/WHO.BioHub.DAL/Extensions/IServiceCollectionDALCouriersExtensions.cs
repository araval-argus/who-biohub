using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALCouriersExtensions
{
    public static IServiceCollection AddDALCouriers(this IServiceCollection services)
    {
        services
            .AddScoped<ICourierReadRepository, SQLCourierReadRepository>()
            .AddScoped<ICourierWriteRepository, SQLCourierWriteRepository>();

        return services;
    }
}