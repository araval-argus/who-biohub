using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.WorklistToBioHubHistoryItems;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALWorklistToBioHubHistoryItemsExtensions
{
    public static IServiceCollection AddDALWorklistToBioHubHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistToBioHubHistoryItemReadRepository, SQLWorklistToBioHubHistoryItemReadRepository>()
            .AddScoped<IWorklistToBioHubHistoryItemWriteRepository, SQLWorklistToBioHubHistoryItemWriteRepository>();

        return services;
    }
}