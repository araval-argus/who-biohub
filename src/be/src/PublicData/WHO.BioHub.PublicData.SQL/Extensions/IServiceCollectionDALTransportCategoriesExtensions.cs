using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALTransportCategoriesExtensions
{
    public static IServiceCollection AddPublicDALTransportCategories(this IServiceCollection services)
    {
        services
            .AddScoped<ITransportCategoryPublicReadRepository, SQLTransportCategoryPublicReadRepository>();

        return services;
    }
}