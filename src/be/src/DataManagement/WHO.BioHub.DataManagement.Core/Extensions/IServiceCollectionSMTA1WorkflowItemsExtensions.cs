using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DeleteSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.DownloadSMTA1WorkflowItemFile;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListDashboardSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListDashboardSMTA1WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ListSMTA1WorkflowItems;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.ReadSMTA1WorkflowItem;
using WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSMTA1WorkflowItemsExtensions
{
    public static IServiceCollection AddCoreSMTA1WorkflowItems(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateSMTA1WorkflowItemHandler, CreateSMTA1WorkflowItemHandler>()
            .AddScoped<ICreateSMTA1WorkflowItemMapper, CreateSMTA1WorkflowItemMapper>()
            .AddScoped<CreateSMTA1WorkflowItemCommandValidator>()

            .AddScoped<IReadSMTA1WorkflowItemHandler, ReadSMTA1WorkflowItemHandler>()
            .AddScoped<IReadSMTA1WorkflowItemMapper, ReadSMTA1WorkflowItemMapper>()
            .AddScoped<ReadSMTA1WorkflowItemQueryValidator>()

            .AddScoped<IDownloadSMTA1WorkflowItemFileHandler, DownloadSMTA1WorkflowItemFileHandler>()
            .AddScoped<DownloadSMTA1WorkflowItemFileQueryValidator>()

            .AddScoped<IUpdateSMTA1WorkflowItemHandler, UpdateSMTA1WorkflowItemHandler>()
            .AddScoped<IUpdateSMTA1WorkflowItemMapper, UpdateSMTA1WorkflowItemMapper>()
            .AddScoped<UpdateSMTA1WorkflowItemCommandValidator>()

            .AddScoped<IDeleteSMTA1WorkflowItemHandler, DeleteSMTA1WorkflowItemHandler>()
            .AddScoped<DeleteSMTA1WorkflowItemCommandValidator>()

            .AddScoped<IListSMTA1WorkflowItemsHandler, ListSMTA1WorkflowItemsHandler>()
            .AddScoped<IListSMTA1WorkflowItemMapper, ListSMTA1WorkflowItemMapper>()
            .AddScoped<ListSMTA1WorkflowItemsQueryValidator>()

            .AddScoped<IListDashboardSMTA1WorkflowItemsHandler, ListDashboardSMTA1WorkflowItemsHandler>()
            .AddScoped<IListDashboardSMTA1WorkflowItemMapper, ListDashboardSMTA1WorkflowItemMapper>()
            .AddScoped<ListDashboardSMTA1WorkflowItemsQueryValidator>()

            ;



        return services;
    }
}