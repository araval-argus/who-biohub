using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.CreatePriorityRequestType;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.DeletePriorityRequestType;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ListPriorityRequestTypes;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.ReadPriorityRequestType;
using WHO.BioHub.DataManagement.Core.UseCases.PriorityRequestTypes.UpdatePriorityRequestType;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionPriorityRequestTypesExtensions
{
    public static IServiceCollection AddCorePriorityRequestTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICreatePriorityRequestTypeHandler, CreatePriorityRequestTypeHandler>()
            .AddScoped<ICreatePriorityRequestTypeMapper, CreatePriorityRequestTypeMapper>()
            .AddScoped<CreatePriorityRequestTypeCommandValidator>()

            .AddScoped<IReadPriorityRequestTypeHandler, ReadPriorityRequestTypeHandler>()
            .AddScoped<ReadPriorityRequestTypeQueryValidator>()

            .AddScoped<IUpdatePriorityRequestTypeHandler, UpdatePriorityRequestTypeHandler>()
            .AddScoped<IUpdatePriorityRequestTypeMapper, UpdatePriorityRequestTypeMapper>()
            .AddScoped<UpdatePriorityRequestTypeCommandValidator>()

            .AddScoped<IDeletePriorityRequestTypeHandler, DeletePriorityRequestTypeHandler>()
            .AddScoped<DeletePriorityRequestTypeCommandValidator>()

            .AddScoped<IListPriorityRequestTypesHandler, ListPriorityRequestTypesHandler>()
            .AddScoped<ListPriorityRequestTypesQueryValidator>()
            ;

        return services;
    }
}