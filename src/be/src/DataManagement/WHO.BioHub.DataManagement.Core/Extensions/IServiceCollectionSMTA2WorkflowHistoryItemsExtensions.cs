using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.CreateSMTA2WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DeleteSMTA2WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.DownloadSMTA2WorkflowHistoryItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ListSMTA2WorkflowHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.ReadSMTA2WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.UpdateSMTA2WorkflowHistoryItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSMTA2WorkflowHistoryItemsExtensions
{
    public static IServiceCollection AddCoreSMTA2WorkflowHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateSMTA2WorkflowHistoryItemHandler, CreateSMTA2WorkflowHistoryItemHandler>()
            .AddScoped<ICreateSMTA2WorkflowHistoryItemMapper, CreateSMTA2WorkflowHistoryItemMapper>()
            .AddScoped<CreateSMTA2WorkflowHistoryItemCommandValidator>()

            .AddScoped<IReadSMTA2WorkflowHistoryItemHandler, ReadSMTA2WorkflowHistoryItemHandler>()
            .AddScoped<IReadSMTA2WorkflowHistoryItemMapper, ReadSMTA2WorkflowHistoryItemMapper>()
            .AddScoped<ReadSMTA2WorkflowHistoryItemQueryValidator>()

            .AddScoped<IUpdateSMTA2WorkflowHistoryItemHandler, UpdateSMTA2WorkflowHistoryItemHandler>()
            .AddScoped<IUpdateSMTA2WorkflowHistoryItemMapper, UpdateSMTA2WorkflowHistoryItemMapper>()
            .AddScoped<UpdateSMTA2WorkflowHistoryItemCommandValidator>()

            .AddScoped<IDeleteSMTA2WorkflowHistoryItemHandler, DeleteSMTA2WorkflowHistoryItemHandler>()
            .AddScoped<DeleteSMTA2WorkflowHistoryItemCommandValidator>()

            .AddScoped<IListSMTA2WorkflowHistoryItemsHandler, ListSMTA2WorkflowHistoryItemsHandler>()
            .AddScoped<IListSMTA2WorkflowHistoryItemMapper, ListSMTA2WorkflowHistoryItemMapper>()
            .AddScoped<ListSMTA2WorkflowHistoryItemsQueryValidator>()

            .AddScoped<IDownloadSMTA2WorkflowHistoryItemFileHandler, DownloadSMTA2WorkflowHistoryItemFileHandler>()
            .AddScoped<DownloadSMTA2WorkflowHistoryItemFileQueryValidator>()
            ;

        return services;
    }
}