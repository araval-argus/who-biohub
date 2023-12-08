using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.CreateSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DeleteSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.DownloadSMTA2WorkflowItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListDashboardSMTA2WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ReadSMTA2WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSMTA2WorkflowItemsExtensions
{
    public static IServiceCollection AddCoreSMTA2WorkflowItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateSMTA2WorkflowItemHandler, CreateSMTA2WorkflowItemHandler>()
            .AddScoped<ICreateSMTA2WorkflowItemMapper, CreateSMTA2WorkflowItemMapper>()
            .AddScoped<CreateSMTA2WorkflowItemCommandValidator>()

            .AddScoped<IReadSMTA2WorkflowItemHandler, ReadSMTA2WorkflowItemHandler>()
            .AddScoped<IReadSMTA2WorkflowItemMapper, ReadSMTA2WorkflowItemMapper>()
            .AddScoped<ReadSMTA2WorkflowItemQueryValidator>()

            .AddScoped<IUpdateSMTA2WorkflowItemHandler, UpdateSMTA2WorkflowItemHandler>()
            .AddScoped<IUpdateSMTA2WorkflowItemMapper, UpdateSMTA2WorkflowItemMapper>()
            .AddScoped<UpdateSMTA2WorkflowItemCommandValidator>()

            .AddScoped<IDeleteSMTA2WorkflowItemHandler, DeleteSMTA2WorkflowItemHandler>()
            .AddScoped<DeleteSMTA2WorkflowItemCommandValidator>()

            .AddScoped<IListSMTA2WorkflowItemsHandler, ListSMTA2WorkflowItemsHandler>()
            .AddScoped<IListSMTA2WorkflowItemMapper, ListSMTA2WorkflowItemMapper>()
            .AddScoped<ListSMTA2WorkflowItemsQueryValidator>()

            .AddScoped<IDownloadSMTA2WorkflowItemFileHandler, DownloadSMTA2WorkflowItemFileHandler>()
            .AddScoped<DownloadSMTA2WorkflowItemFileQueryValidator>()

            .AddScoped<IListDashboardSMTA2WorkflowItemsHandler, ListDashboardSMTA2WorkflowItemsHandler>()
            .AddScoped<IListDashboardSMTA2WorkflowItemMapper, ListDashboardSMTA2WorkflowItemMapper>()
            .AddScoped<ListDashboardSMTA2WorkflowItemsQueryValidator>()

            ;

        return services;
    }
}