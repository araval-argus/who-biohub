using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.DAL.Repositories.SMTA1WorkflowHistoryItems;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALSMTA1WorkflowHistoryItemsExtensions
{
    public static IServiceCollection AddDALSMTA1WorkflowHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA1WorkflowHistoryItemReadRepository, SQLSMTA1WorkflowHistoryItemReadRepository>()
            .AddScoped<ISMTA1WorkflowHistoryItemWriteRepository, SQLSMTA1WorkflowHistoryItemWriteRepository>();

        return services;
    }
}