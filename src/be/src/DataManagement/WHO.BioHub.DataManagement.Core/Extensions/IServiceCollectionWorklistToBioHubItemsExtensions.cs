using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DeleteWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.DownloadWorklistToBioHubItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListDashboardWorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ReadWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItemShipmentDocuments;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionWorklistToBioHubItemsExtensions
{
    public static IServiceCollection AddCoreWorklistToBioHubItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateWorklistToBioHubItemHandler, CreateWorklistToBioHubItemHandler>()
            .AddScoped<ICreateWorklistToBioHubItemMapper, CreateWorklistToBioHubItemMapper>()
            .AddScoped<CreateWorklistToBioHubItemCommandValidator>()

            .AddScoped<IReadWorklistToBioHubItemHandler, ReadWorklistToBioHubItemHandler>()
            .AddScoped<IReadWorklistToBioHubItemMapper, ReadWorklistToBioHubItemMapper>()
            .AddScoped<ReadWorklistToBioHubItemQueryValidator>()

            .AddScoped<IDownloadWorklistToBioHubItemFileHandler, DownloadWorklistToBioHubItemFileHandler>()
            .AddScoped<DownloadWorklistToBioHubItemFileQueryValidator>()

            .AddScoped<IUpdateWorklistToBioHubItemHandler, UpdateWorklistToBioHubItemHandler>()
            .AddScoped<IUpdateWorklistToBioHubItemMapper, UpdateWorklistToBioHubItemMapper>()
            .AddScoped<UpdateWorklistToBioHubItemCommandValidator>()

            .AddScoped<IUpdateWorklistToBioHubItemShipmentDocumentsHandler, UpdateWorklistToBioHubItemShipmentDocumentsHandler>()
            .AddScoped<IUpdateWorklistToBioHubItemShipmentDocumentsMapper, UpdateWorklistToBioHubItemShipmentDocumentsMapper>()
            .AddScoped<UpdateWorklistToBioHubItemShipmentDocumentsCommandValidator>()

            .AddScoped<IDeleteWorklistToBioHubItemHandler, DeleteWorklistToBioHubItemHandler>()
            .AddScoped<DeleteWorklistToBioHubItemCommandValidator>()

            .AddScoped<IListWorklistToBioHubItemsHandler, ListWorklistToBioHubItemsHandler>()
            .AddScoped<IListWorklistToBioHubItemMapper, ListWorklistToBioHubItemMapper>()
            .AddScoped<ListWorklistToBioHubItemsQueryValidator>()

            .AddScoped<IListDashboardWorklistToBioHubItemsHandler, ListDashboardWorklistToBioHubItemsHandler>()
            .AddScoped<IListDashboardWorklistToBioHubItemMapper, ListDashboardWorklistToBioHubItemMapper>()
            .AddScoped<ListDashboardWorklistToBioHubItemsQueryValidator>()

            ;



        return services;
    }
}