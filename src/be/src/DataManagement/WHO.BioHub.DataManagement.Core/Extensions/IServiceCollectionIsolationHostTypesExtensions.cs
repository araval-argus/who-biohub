using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.DeleteIsolationHostType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ListIsolationHostTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.ReadIsolationHostType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.UpdateIsolationHostType;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionIsolationHostTypesExtensions
{
    public static IServiceCollection AddCoreIsolationHostTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateIsolationHostTypeHandler, CreateIsolationHostTypeHandler>()
            .AddScoped<ICreateIsolationHostTypeMapper, CreateIsolationHostTypeMapper>()
            .AddScoped<CreateIsolationHostTypeCommandValidator>()

            .AddScoped<IReadIsolationHostTypeHandler, ReadIsolationHostTypeHandler>()
            .AddScoped<ReadIsolationHostTypeQueryValidator>()

            .AddScoped<IUpdateIsolationHostTypeHandler, UpdateIsolationHostTypeHandler>()
            .AddScoped<IUpdateIsolationHostTypeMapper, UpdateIsolationHostTypeMapper>()
            .AddScoped<UpdateIsolationHostTypeCommandValidator>()

            .AddScoped<IDeleteIsolationHostTypeHandler, DeleteIsolationHostTypeHandler>()
            .AddScoped<DeleteIsolationHostTypeCommandValidator>()

            .AddScoped<IListIsolationHostTypesHandler, ListIsolationHostTypesHandler>()
            .AddScoped<ListIsolationHostTypesQueryValidator>()
            ;

        return services;
    }
}