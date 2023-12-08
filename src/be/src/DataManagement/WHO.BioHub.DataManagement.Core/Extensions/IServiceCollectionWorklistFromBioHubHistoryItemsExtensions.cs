using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.CreateWorklistFromBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DeleteWorklistFromBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.DownloadWorklistFromBioHubHistoryItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ListWorklistFromBioHubHistoryItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.ReadWorklistFromBioHubHistoryItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.UpdateWorklistFromBioHubHistoryItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionWorklistFromBioHubHistoryItemsExtensions
{
    public static IServiceCollection AddCoreWorklistFromBioHubHistoryItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateWorklistFromBioHubHistoryItemHandler, CreateWorklistFromBioHubHistoryItemHandler>()
            .AddScoped<ICreateWorklistFromBioHubHistoryItemMapper, CreateWorklistFromBioHubHistoryItemMapper>()
            .AddScoped<CreateWorklistFromBioHubHistoryItemCommandValidator>()

            .AddScoped<IReadWorklistFromBioHubHistoryItemHandler, ReadWorklistFromBioHubHistoryItemHandler>()
            .AddScoped<ReadWorklistFromBioHubHistoryItemQueryValidator>()

            .AddScoped<IUpdateWorklistFromBioHubHistoryItemHandler, UpdateWorklistFromBioHubHistoryItemHandler>()
            .AddScoped<IUpdateWorklistFromBioHubHistoryItemMapper, UpdateWorklistFromBioHubHistoryItemMapper>()
            .AddScoped<UpdateWorklistFromBioHubHistoryItemCommandValidator>()

            .AddScoped<IDeleteWorklistFromBioHubHistoryItemHandler, DeleteWorklistFromBioHubHistoryItemHandler>()
            .AddScoped<DeleteWorklistFromBioHubHistoryItemCommandValidator>()

            .AddScoped<IListWorklistFromBioHubHistoryItemsHandler, ListWorklistFromBioHubHistoryItemsHandler>()
            .AddScoped<IListWorklistFromBioHubHistoryItemMapper, ListWorklistFromBioHubHistoryItemMapper>()
            .AddScoped<ListWorklistFromBioHubHistoryItemsQueryValidator>()

            .AddScoped<IDownloadWorklistFromBioHubHistoryItemFileHandler, DownloadWorklistFromBioHubHistoryItemFileHandler>()
            .AddScoped<DownloadWorklistFromBioHubHistoryItemFileQueryValidator>()
            ;

        return services;
    }
}