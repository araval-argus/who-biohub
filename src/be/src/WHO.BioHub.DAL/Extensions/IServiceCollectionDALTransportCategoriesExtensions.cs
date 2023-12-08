using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALTransportCategoriesExtensions
{
    public static IServiceCollection AddDALTransportCategories(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportCategoryReadRepository, SQLTransportCategoryReadRepository>()
            .AddScoped<ITransportCategoryWriteRepository, SQLTransportCategoryWriteRepository>();

        return services;
    }
}