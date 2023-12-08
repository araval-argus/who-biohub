using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubHistoryItems;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALWorklistFromBioHubHistoryItemsExtensions
{
    public static IServiceCollection AddDALWorklistFromBioHubHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistFromBioHubHistoryItemReadRepository, SQLWorklistFromBioHubHistoryItemReadRepository>()
            .AddScoped<IWorklistFromBioHubHistoryItemWriteRepository, SQLWorklistFromBioHubHistoryItemWriteRepository>();

        return services;
    }
}