using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.CreateTransportMode;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.DeleteTransportMode;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ListTransportModes;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.ReadTransportMode;
using WHO.BioHub.DataManagement.Core.UseCases.TransportModes.UpdateTransportMode;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionTransportModesExtensions
{
    public static IServiceCollection AddCoreTransportModes(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateTransportModeHandler, CreateTransportModeHandler>()
            .AddScoped<ICreateTransportModeMapper, CreateTransportModeMapper>()
            .AddScoped<CreateTransportModeCommandValidator>()

            .AddScoped<IReadTransportModeHandler, ReadTransportModeHandler>()
            .AddScoped<ReadTransportModeQueryValidator>()

            .AddScoped<IUpdateTransportModeHandler, UpdateTransportModeHandler>()
            .AddScoped<IUpdateTransportModeMapper, UpdateTransportModeMapper>()
            .AddScoped<UpdateTransportModeCommandValidator>()

            .AddScoped<IDeleteTransportModeHandler, DeleteTransportModeHandler>()
            .AddScoped<DeleteTransportModeCommandValidator>()

            .AddScoped<IListTransportModesHandler, ListTransportModesHandler>()
            .AddScoped<ListTransportModesQueryValidator>()
            ;

        return services;
    }
}