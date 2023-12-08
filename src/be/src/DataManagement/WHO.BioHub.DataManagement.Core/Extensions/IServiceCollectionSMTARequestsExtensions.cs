using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SMTARequests.ListSMTARequests;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSMTARequestsExtensions
{
    public static IServiceCollection AddCoreSMTARequests(this IServiceCollection services)
    {
        services

            .AddScoped<IListSMTARequestsHandler, ListSMTARequestsHandler>()
            .AddScoped<IListSMTARequestsMapper, ListSMTARequestsMapper>()
            .AddScoped<ListSMTARequestsQueryValidator>()
            ;

        return services;
    }
}
