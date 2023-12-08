using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.CreateBioHubFacility;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.DeleteBioHubFacility;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListBioHubFacilities;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ListMapBioHubFacilities;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.ReadBioHubFacility;
using WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.UpdateBioHubFacility;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadBioHubFacility;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionBioHubFacilitiesExtensions
{
    public static IServiceCollection AddCoreBioHubFacilities(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateBioHubFacilityHandler, CreateBioHubFacilityHandler>()
            .AddScoped<ICreateBioHubFacilityMapper, CreateBioHubFacilityMapper>()
            .AddScoped<CreateBioHubFacilityCommandValidator>()

            .AddScoped<IReadBioHubFacilityHandler, ReadBioHubFacilityHandler>()
            .AddScoped<IReadBioHubFacilityMapper, ReadBioHubFacilityMapper>()            
            .AddScoped<ReadBioHubFacilityQueryValidator>()

            .AddScoped<IUpdateBioHubFacilityHandler, UpdateBioHubFacilityHandler>()
            .AddScoped<IUpdateBioHubFacilityMapper, UpdateBioHubFacilityMapper>()
            .AddScoped<UpdateBioHubFacilityCommandValidator>()

            .AddScoped<IDeleteBioHubFacilityHandler, DeleteBioHubFacilityHandler>()
            .AddScoped<DeleteBioHubFacilityCommandValidator>()

            .AddScoped<IListBioHubFacilitiesHandler, ListBioHubFacilitiesHandler>()
            .AddScoped<IListBioHubFacilityMapper, ListBioHubFacilityMapper>()
            .AddScoped<ListBioHubFacilitiesQueryValidator>()

            .AddScoped<IListMapBioHubFacilitiesHandler, ListMapBioHubFacilitiesHandler>()
            .AddScoped<IListMapBioHubFacilityMapper, ListMapBioHubFacilityMapper>()
            .AddScoped<ListMapBioHubFacilitiesQueryValidator>()

            ;

        return services;
    }
}