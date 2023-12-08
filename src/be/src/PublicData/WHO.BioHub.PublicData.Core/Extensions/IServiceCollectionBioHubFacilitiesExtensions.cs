using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListBioHubFacilities;
using WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;
using WHO.BioHub.PublicData.Core.UseCases.BioHubFacilities.ReadBioHubFacility;
using WHO.BioHub.PublicData.Core.UseCases.MapBioHubFacilities.ListMapBioHubFacilities;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionBioHubFacilitiesExtensions
{
    public static IServiceCollection AddCoreBioHubFacilities(this IServiceCollection services)
    {
        services
            .AddScoped<IReadBioHubFacilityHandler, ReadBioHubFacilityHandler>()
            .AddScoped<ReadBioHubFacilityQueryValidator>()

            .AddScoped<IListBioHubFacilitiesHandler, ListBioHubFacilitiesHandler>()
            .AddScoped<ListBioHubFacilitiesQueryValidator>()

            .AddScoped<IListMapBioHubFacilitiesHandler, ListMapBioHubFacilitiesHandler>()
            .AddScoped<IListMapBioHubFacilityMapper, ListMapBioHubFacilityMapper>()
            .AddScoped<ListMapBioHubFacilitiesQueryValidator>()

            .AddScoped<IReadBioHubFacilityMapper, ReadBioHubFacilityMapper>()
            .AddScoped<IListBioHubFacilityMapper, ListBioHubFacilityMapper>()
            ;

        return services;
    }
}