using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowItems;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALSMTA2WorkflowItemsExtensions
{
    public static IServiceCollection AddDALSMTA2WorkflowItems(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA2WorkflowItemReadRepository, SQLSMTA2WorkflowItemReadRepository>()
            .AddScoped<ISMTA2WorkflowItemWriteRepository, SQLSMTA2WorkflowItemWriteRepository>();

        return services;
    }
}