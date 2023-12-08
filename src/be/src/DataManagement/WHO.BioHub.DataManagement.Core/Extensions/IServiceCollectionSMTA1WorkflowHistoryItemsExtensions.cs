using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.CreateSMTA1WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DeleteSMTA1WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.DownloadSMTA1WorkflowHistoryItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ListSMTA1WorkflowHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.ReadSMTA1WorkflowHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.UpdateSMTA1WorkflowHistoryItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSMTA1WorkflowHistoryItemsExtensions
{
    public static IServiceCollection AddCoreSMTA1WorkflowHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateSMTA1WorkflowHistoryItemHandler, CreateSMTA1WorkflowHistoryItemHandler>()
            .AddScoped<ICreateSMTA1WorkflowHistoryItemMapper, CreateSMTA1WorkflowHistoryItemMapper>()
            .AddScoped<CreateSMTA1WorkflowHistoryItemCommandValidator>()

            .AddScoped<IReadSMTA1WorkflowHistoryItemHandler, ReadSMTA1WorkflowHistoryItemHandler>()
            .AddScoped<IReadSMTA1WorkflowHistoryItemMapper, ReadSMTA1WorkflowHistoryItemMapper>()
            .AddScoped<ReadSMTA1WorkflowHistoryItemQueryValidator>()

            .AddScoped<IUpdateSMTA1WorkflowHistoryItemHandler, UpdateSMTA1WorkflowHistoryItemHandler>()
            .AddScoped<IUpdateSMTA1WorkflowHistoryItemMapper, UpdateSMTA1WorkflowHistoryItemMapper>()
            .AddScoped<UpdateSMTA1WorkflowHistoryItemCommandValidator>()

            .AddScoped<IDeleteSMTA1WorkflowHistoryItemHandler, DeleteSMTA1WorkflowHistoryItemHandler>()
            .AddScoped<DeleteSMTA1WorkflowHistoryItemCommandValidator>()

            .AddScoped<IListSMTA1WorkflowHistoryItemsHandler, ListSMTA1WorkflowHistoryItemsHandler>()
            .AddScoped<IListSMTA1WorkflowHistoryItemMapper, ListSMTA1WorkflowHistoryItemMapper>()
            .AddScoped<ListSMTA1WorkflowHistoryItemsQueryValidator>()

            .AddScoped<IDownloadSMTA1WorkflowHistoryItemFileHandler, DownloadSMTA1WorkflowHistoryItemFileHandler>()
            .AddScoped<DownloadSMTA1WorkflowHistoryItemFileQueryValidator>()
            ;




        return services;
    }
}