using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;
using WHO.BioHub.PublicData.Core.UseCases.PriorityRequestTypes.ReadPriorityRequestType;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionPriorityRequestTypesExtensions
{
    public static IServiceCollection AddCorePriorityRequestTypes(this IServiceCollection services)
    {
        services
            .AddScoped<IReadPriorityRequestTypeHandler, ReadPriorityRequestTypeHandler>()
            .AddScoped<ReadPriorityRequestTypeQueryValidator>()

            .AddScoped<IListPriorityRequestTypesHandler, ListPriorityRequestTypesHandler>()
            .AddScoped<ListPriorityRequestTypesQueryValidator>()
            ;

        return services;
    }
}