using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.CreateMaterialShippingInformationHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.DeleteMaterialShippingInformationHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ListMaterialShippingInformationsHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ReadMaterialShippingInformation;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.ReadMaterialShippingInformationHistory;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.UpdateMaterialShippingInformationHistory;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialShippingInformationsHistoryExtensions
{
    public static IServiceCollection AddCoreMaterialShippingInformationsHistory(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialShippingInformationHistoryHandler, CreateMaterialShippingInformationHistoryHandler>()
            .AddScoped<ICreateMaterialShippingInformationHistoryMapper, CreateMaterialShippingInformationHistoryMapper>()
            .AddScoped<CreateMaterialShippingInformationHistoryCommandValidator>()

            .AddScoped<IReadMaterialShippingInformationHistoryHandler, ReadMaterialShippingInformationHistoryHandler>()
            .AddScoped<ReadMaterialShippingInformationHistoryQueryValidator>()

            .AddScoped<IUpdateMaterialShippingInformationHistoryHandler, UpdateMaterialShippingInformationHistoryHandler>()
            .AddScoped<IUpdateMaterialShippingInformationHistoryMapper, UpdateMaterialShippingInformationHistoryMapper>()
            .AddScoped<UpdateMaterialShippingInformationHistoryCommandValidator>()

            .AddScoped<IDeleteMaterialShippingInformationHistoryHandler, DeleteMaterialShippingInformationHistoryHandler>()
            .AddScoped<DeleteMaterialShippingInformationHistoryCommandValidator>()

            .AddScoped<IListMaterialShippingInformationsHistoryHandler, ListMaterialShippingInformationsHistoryHandler>()
            .AddScoped<ListMaterialShippingInformationsHistoryQueryValidator>()
            ;

        return services;
    }
}