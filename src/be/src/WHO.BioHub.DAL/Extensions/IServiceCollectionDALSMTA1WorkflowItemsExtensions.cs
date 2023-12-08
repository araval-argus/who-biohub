using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowItems;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALSMTA1WorkflowItemsExtensions
{
    public static IServiceCollection AddDALSMTA1WorkflowItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA1WorkflowItemReadRepository, SQLSMTA1WorkflowItemReadRepository>()
            .AddScoped<ISMTA1WorkflowItemWriteRepository, SQLSMTA1WorkflowItemWriteRepository>();

        return services;
    }
}