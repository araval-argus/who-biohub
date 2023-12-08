using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.CreateMaterialClinicalDetailHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.DeleteMaterialClinicalDetailHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ListMaterialClinicalDetailsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ReadMaterialClinicalDetailHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.UpdateMaterialClinicalDetailHistory;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialClinicalDetailsHistoryExtensions
{
    public static IServiceCollection AddCoreMaterialClinicalDetailsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialClinicalDetailHistoryHandler, CreateMaterialClinicalDetailHistoryHandler>()
            .AddScoped<ICreateMaterialClinicalDetailHistoryMapper, CreateMaterialClinicalDetailHistoryMapper>()
            .AddScoped<CreateMaterialClinicalDetailHistoryCommandValidator>()

            .AddScoped<IReadMaterialClinicalDetailHistoryHandler, ReadMaterialClinicalDetailHistoryHandler>()
            .AddScoped<ReadMaterialClinicalDetailHistoryQueryValidator>()

            .AddScoped<IUpdateMaterialClinicalDetailHistoryHandler, UpdateMaterialClinicalDetailHistoryHandler>()
            .AddScoped<IUpdateMaterialClinicalDetailHistoryMapper, UpdateMaterialClinicalDetailHistoryMapper>()
            .AddScoped<UpdateMaterialClinicalDetailHistoryCommandValidator>()

            .AddScoped<IDeleteMaterialClinicalDetailHistoryHandler, DeleteMaterialClinicalDetailHistoryHandler>()
            .AddScoped<DeleteMaterialClinicalDetailHistoryCommandValidator>()

            .AddScoped<IListMaterialClinicalDetailsHistoryHandler, ListMaterialClinicalDetailsHistoryHandler>()
            .AddScoped<ListMaterialClinicalDetailsHistoryQueryValidator>()
            ;

        return services;
    }
}