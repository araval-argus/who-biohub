using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DeleteWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.DownloadWorklistFromBioHubItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListDashboardWorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ListWorklistFromBioHubItems;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.ReadWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItem;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemBHFShipmentDocuments;
using WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemQEShipmentDocuments;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionWorklistFromBioHubItemsExtensions
{
    public static IServiceCollection AddCoreWorklistFromBioHubItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateWorklistFromBioHubItemHandler, CreateWorklistFromBioHubItemHandler>()
            .AddScoped<ICreateWorklistFromBioHubItemMapper, CreateWorklistFromBioHubItemMapper>()
            .AddScoped<CreateWorklistFromBioHubItemCommandValidator>()

            .AddScoped<IReadWorklistFromBioHubItemHandler, ReadWorklistFromBioHubItemHandler>()
            .AddScoped<IReadWorklistFromBioHubItemMapper, ReadWorklistFromBioHubItemMapper>()
            .AddScoped<ReadWorklistFromBioHubItemQueryValidator>()

            .AddScoped<IUpdateWorklistFromBioHubItemHandler, UpdateWorklistFromBioHubItemHandler>()
            .AddScoped<IUpdateWorklistFromBioHubItemMapper, UpdateWorklistFromBioHubItemMapper>()
            .AddScoped<UpdateWorklistFromBioHubItemCommandValidator>()

            .AddScoped<IUpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler, UpdateWorklistFromBioHubItemBHFShipmentDocumentsHandler>()
            .AddScoped<IUpdateWorklistFromBioHubItemBHFShipmentDocumentsMapper, UpdateWorklistFromBioHubItemBHFShipmentDocumentsMapper>()
            .AddScoped<UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommandValidator>()

            .AddScoped<IUpdateWorklistFromBioHubItemQEShipmentDocumentsHandler, UpdateWorklistFromBioHubItemQEShipmentDocumentsHandler>()
            .AddScoped<IUpdateWorklistFromBioHubItemQEShipmentDocumentsMapper, UpdateWorklistFromBioHubItemQEShipmentDocumentsMapper>()
            .AddScoped<UpdateWorklistFromBioHubItemQEShipmentDocumentsCommandValidator>()


            .AddScoped<IDeleteWorklistFromBioHubItemHandler, DeleteWorklistFromBioHubItemHandler>()
            .AddScoped<DeleteWorklistFromBioHubItemCommandValidator>()

            .AddScoped<IListWorklistFromBioHubItemsHandler, ListWorklistFromBioHubItemsHandler>()
            .AddScoped<IListWorklistFromBioHubItemMapper, ListWorklistFromBioHubItemMapper>()
            .AddScoped<ListWorklistFromBioHubItemsQueryValidator>()

            .AddScoped<IListDashboardWorklistFromBioHubItemsHandler, ListDashboardWorklistFromBioHubItemsHandler>()
            .AddScoped<IListDashboardWorklistFromBioHubItemMapper, ListDashboardWorklistFromBioHubItemMapper>()
            .AddScoped<ListDashboardWorklistFromBioHubItemsQueryValidator>()


            .AddScoped<IDownloadWorklistFromBioHubItemFileHandler, DownloadWorklistFromBioHubItemFileHandler>()
            .AddScoped<DownloadWorklistFromBioHubItemFileQueryValidator>()
            ;


        return services;
    }
}