using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.CreateMaterialClinicalDetail;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.DeleteMaterialClinicalDetail;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ListMaterialClinicalDetails;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ReadMaterialClinicalDetail;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.UpdateMaterialClinicalDetail;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialClinicalDetailsExtensions
{
    public static IServiceCollection AddCoreMaterialClinicalDetails(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialClinicalDetailHandler, CreateMaterialClinicalDetailHandler>()
            .AddScoped<ICreateMaterialClinicalDetailMapper, CreateMaterialClinicalDetailMapper>()
            .AddScoped<CreateMaterialClinicalDetailCommandValidator>()

            .AddScoped<IReadMaterialClinicalDetailHandler, ReadMaterialClinicalDetailHandler>()
            .AddScoped<ReadMaterialClinicalDetailQueryValidator>()

            .AddScoped<IUpdateMaterialClinicalDetailHandler, UpdateMaterialClinicalDetailHandler>()
            .AddScoped<IUpdateMaterialClinicalDetailMapper, UpdateMaterialClinicalDetailMapper>()
            .AddScoped<UpdateMaterialClinicalDetailCommandValidator>()

            .AddScoped<IDeleteMaterialClinicalDetailHandler, DeleteMaterialClinicalDetailHandler>()
            .AddScoped<DeleteMaterialClinicalDetailCommandValidator>()

            .AddScoped<IListMaterialClinicalDetailsHandler, ListMaterialClinicalDetailsHandler>()
            .AddScoped<ListMaterialClinicalDetailsQueryValidator>()
            ;

        return services;
    }
}