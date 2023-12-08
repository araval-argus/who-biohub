using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ListTransportCategories;
using WHO.BioHub.PublicData.Core.UseCases.TransportCategories.ReadTransportCategory;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionTransportCategoriesExtensions
{
    public static IServiceCollection AddCoreTransportCategories(this IServiceCollection services)
    {
        services
            .AddScoped<IReadTransportCategoryHandler, ReadTransportCategoryHandler>()
            .AddScoped<ReadTransportCategoryQueryValidator>()

            .AddScoped<IListTransportCategoriesHandler, ListTransportCategoriesHandler>()
            .AddScoped<ListTransportCategoriesQueryValidator>()
            ;

        return services;
    }
}