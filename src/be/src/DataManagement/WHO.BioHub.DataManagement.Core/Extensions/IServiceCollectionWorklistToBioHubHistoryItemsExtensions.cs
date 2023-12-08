using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.CreateWorklistToBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DeleteWorklistToBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.DownloadWorklistToBioHubHistoryItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ListWorklistToBioHubHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.ReadWorklistToBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.UpdateWorklistToBioHubHistoryItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionWorklistToBioHubHistoryItemsExtensions
{
    public static IServiceCollection AddCoreWorklistToBioHubHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateWorklistToBioHubHistoryItemHandler, CreateWorklistToBioHubHistoryItemHandler>()
            .AddScoped<ICreateWorklistToBioHubHistoryItemMapper, CreateWorklistToBioHubHistoryItemMapper>()
            .AddScoped<CreateWorklistToBioHubHistoryItemCommandValidator>()

            .AddScoped<IReadWorklistToBioHubHistoryItemHandler, ReadWorklistToBioHubHistoryItemHandler>()
            .AddScoped<IReadWorklistToBioHubHistoryItemMapper, ReadWorklistToBioHubHistoryItemMapper>()
            .AddScoped<ReadWorklistToBioHubHistoryItemQueryValidator>()

            .AddScoped<IUpdateWorklistToBioHubHistoryItemHandler, UpdateWorklistToBioHubHistoryItemHandler>()
            .AddScoped<IUpdateWorklistToBioHubHistoryItemMapper, UpdateWorklistToBioHubHistoryItemMapper>()
            .AddScoped<UpdateWorklistToBioHubHistoryItemCommandValidator>()

            .AddScoped<IDeleteWorklistToBioHubHistoryItemHandler, DeleteWorklistToBioHubHistoryItemHandler>()
            .AddScoped<DeleteWorklistToBioHubHistoryItemCommandValidator>()

            .AddScoped<IListWorklistToBioHubHistoryItemsHandler, ListWorklistToBioHubHistoryItemsHandler>()
            .AddScoped<IListWorklistToBioHubHistoryItemMapper, ListWorklistToBioHubHistoryItemMapper>()
            .AddScoped<ListWorklistToBioHubHistoryItemsQueryValidator>()

            .AddScoped<IDownloadWorklistToBioHubHistoryItemFileHandler, DownloadWorklistToBioHubHistoryItemFileHandler>()
            .AddScoped<DownloadWorklistToBioHubHistoryItemFileQueryValidator>()
            ;




        return services;
    }
}