using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowHistoryItems;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALSMTA2WorkflowHistoryItemsExtensions
{
    public static IServiceCollection AddDALSMTA2WorkflowHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA2WorkflowHistoryItemReadRepository, SQLSMTA2WorkflowHistoryItemReadRepository>()
            .AddScoped<ISMTA2WorkflowHistoryItemWriteRepository, SQLSMTA2WorkflowHistoryItemWriteRepository>();

        return services;
    }
}