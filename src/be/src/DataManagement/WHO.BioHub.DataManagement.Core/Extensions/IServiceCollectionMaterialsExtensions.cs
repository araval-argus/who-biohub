using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialEvents.ListMaterialEvents;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.CreateMaterial;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.DeleteMaterial;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterial;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForBioHubFacilityCompletion;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForLaboratoryCompletion;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterial;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForBioHubFacilityCompletion;
using WHO.BioHub.DataManagement.Core.UseCases.Materials.UpdateMaterialForLaboratoryCompletion;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialsExtensions
{
    public static IServiceCollection AddCoreMaterials(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialHandler, CreateMaterialHandler>()
            .AddScoped<ICreateMaterialMapper, CreateMaterialMapper>()
            .AddScoped<CreateMaterialCommandValidator>()

            .AddScoped<IReadMaterialHandler, ReadMaterialHandler>()
            .AddScoped<IReadMaterialMapper, ReadMaterialMapper>()
            .AddScoped<ReadMaterialQueryValidator>()

            .AddScoped<IUpdateMaterialHandler, UpdateMaterialHandler>()
            .AddScoped<IUpdateMaterialMapper, UpdateMaterialMapper>()
            .AddScoped<UpdateMaterialCommandValidator>()

            .AddScoped<IDeleteMaterialHandler, DeleteMaterialHandler>()
            .AddScoped<DeleteMaterialCommandValidator>()

            .AddScoped<IListMaterialsHandler, ListMaterialsHandler>()
            .AddScoped<IListMaterialsMapper, ListMaterialsMapper>()
            .AddScoped<ListMaterialsQueryValidator>()

            .AddScoped<IListMaterialsForWorklistFromBioHubItemHandler, ListMaterialsForWorklistFromBioHubItemHandler>()
            .AddScoped<ListMaterialsForWorklistFromBioHubItemQueryValidator>()

            .AddScoped<IListMaterialsForWorklistToBioHubItemHandler, ListMaterialsForWorklistToBioHubItemHandler>()
            .AddScoped<ListMaterialsForWorklistToBioHubItemQueryValidator>()

            .AddScoped<IReadMaterialForLaboratoryCompletionHandler, ReadMaterialForLaboratoryCompletionHandler>()
            .AddScoped<IReadMaterialForLaboratoryCompletionMapper, ReadMaterialForLaboratoryCompletionMapper>()
            .AddScoped<ReadMaterialForLaboratoryCompletionQueryValidator>()


            .AddScoped<IUpdateMaterialForLaboratoryCompletionHandler, UpdateMaterialForLaboratoryCompletionHandler>()
            .AddScoped<IUpdateMaterialForLaboratoryCompletionMapper, UpdateMaterialForLaboratoryCompletionMapper>()
            .AddScoped<UpdateMaterialForLaboratoryCompletionCommandValidator>()


            .AddScoped<IReadMaterialForBioHubFacilityCompletionHandler, ReadMaterialForBioHubFacilityCompletionHandler>()
            .AddScoped<IReadMaterialForBioHubFacilityCompletionMapper, ReadMaterialForBioHubFacilityCompletionMapper>()
            .AddScoped<ReadMaterialForBioHubFacilityCompletionQueryValidator>()


            .AddScoped<IUpdateMaterialForBioHubFacilityCompletionHandler, UpdateMaterialForBioHubFacilityCompletionHandler>()
            .AddScoped<IUpdateMaterialForBioHubFacilityCompletionMapper, UpdateMaterialForBioHubFacilityCompletionMapper>()
            .AddScoped<UpdateMaterialForBioHubFacilityCompletionCommandValidator>()

            .AddScoped<IListMaterialEventsHandler, ListMaterialEventsHandler>()
            .AddScoped<IListMaterialEventsMapper, ListMaterialEventsMapper>()
            .AddScoped<ListMaterialEventsQueryValidator>()
            

            ;




        return services;
    }
}