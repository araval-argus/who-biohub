using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALWorklistItemUsedReferenceNumbersExtensions
{
    public static IServiceCollection AddDALWorklistItemUsedReferenceNumbers(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistItemUsedReferenceNumberReadRepository, SQLWorklistItemUsedReferenceNumberReadRepository>()
            .AddScoped<IWorklistItemUsedReferenceNumberWriteRepository, SQLWorklistItemUsedReferenceNumberWriteRepository>();

        return services;
    }
}