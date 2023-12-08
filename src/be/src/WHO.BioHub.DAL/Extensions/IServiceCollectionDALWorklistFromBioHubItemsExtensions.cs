using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALWorklistFromBioHubItemsExtensions
{
    public static IServiceCollection AddDALWorklistFromBioHubItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistFromBioHubItemReadRepository, SQLWorklistFromBioHubItemReadRepository>()
            .AddScoped<IWorklistFromBioHubItemWriteRepository, SQLWorklistFromBioHubItemWriteRepository>();

        return services;
    }
}