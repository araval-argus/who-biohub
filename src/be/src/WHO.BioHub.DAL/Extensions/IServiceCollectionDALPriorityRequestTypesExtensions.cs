using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.PriorityRequestTypes;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALPriorityRequestTypesExtensions
{
    public static IServiceCollection AddDALPriorityRequestTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IPriorityRequestTypeReadRepository, SQLPriorityRequestTypeReadRepository>()
            .AddScoped<IPriorityRequestTypeWriteRepository, SQLPriorityRequestTypeWriteRepository>();

        return services;
    }
}