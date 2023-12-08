using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.WorklistToBioHubItems;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALWorklistToBioHubItemsExtensions
{
    public static IServiceCollection AddDALWorklistToBioHubItems(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistToBioHubItemReadRepository, SQLWorklistToBioHubItemReadRepository>()
            .AddScoped<IWorklistToBioHubItemWriteRepository, SQLWorklistToBioHubItemWriteRepository>();

        return services;
    }
}